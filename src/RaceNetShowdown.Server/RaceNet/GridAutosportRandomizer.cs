using System;
using System.Collections.Generic;
using System.Linq;
using static GridAutosportData;

public static class GridAutosportRandomizer
{
    private static readonly Random Rnd = new Random();

    // Track model -> available conditions, from LocationToTracksMap.
    private static readonly IReadOnlyDictionary<GridAutosportTrackModel, List<GridAutosportTrackModelConditions>> TrackConditions =
        LocationToTracksMap.Values
            .SelectMany(tracks => tracks)
            .Where(t => t.Conditions.Count > 0)
            .ToDictionary(t => t.Track, t => t.Conditions);

    public static GridAutoSportChallengeRace[] GenerateRandomRaces(int totalRaces = 6)
    {
        if (totalRaces < 5)
            throw new ArgumentException("Sono necessarie almeno 5 gare.");

        // One race per discipline, then fill the remaining slots randomly.
        var allDisciplines = DisciplineToVehicleClasses.Keys.ToArray();
        var disciplines = allDisciplines.ToList();
        while (disciplines.Count < totalRaces)
            disciplines.Add(Pick(allDisciplines));
        // Always Touring, Endurance, Openwheel, Tuner, Street (enum order).
        disciplines = disciplines.OrderBy(d => (int)d).ToList();

        var usedTracks = new HashSet<GridAutosportTrackModel>();
        var generatedRaces = new List<GridAutoSportChallengeRace>();

        for (int i = 0; i < totalRaces; i++)
        {
            var discipline = disciplines[i];
            var vehicleClass = PickVehicleClass(discipline);
            var raceType = Pick(VehicleClassToRaceTypes[vehicleClass].Where(RaceTypeToTracks.ContainsKey).ToArray());
            var trackModel = PickTrack(raceType, usedTracks);
            var condition = Pick(TrackConditions[trackModel]);
            usedTracks.Add(trackModel);

            bool specialRace = discipline == GridAutosportDisciplineID.Endurance;
            var targets = GenerateTargets(raceType, vehicleClass, trackModel);

            generatedRaces.Add(new GridAutoSportChallengeRace(
                i + 1, targets.HigherIsBetter, discipline, condition, raceType, 0, -1, vehicleClass, 0, 0,
                specialRace, 1, -1, targets.PersonalBest,
                targets.Platinum, targets.Gold, targets.Silver, targets.Bronze
            ));
        }

        return generatedRaces.ToArray();
    }

    // ---- Medal targets ----------------------------------------------------------------------
    // Times are in milliseconds (confirmed by DataMining.EndRace logs: 3-lap race = 329460).

    /// <summary>Laps counted in the target time for a Race event (RaceDuration = 0 uses the default).</summary>
    private const int RaceLaps = 3;

    /// <summary>Where in the class mph range (min..max) a platinum run sits.</summary>
    private const double PlatinumSpeedFraction = 0.75;

    /// <summary>Street circuits / city routes are slower than the class average.</summary>
    private const double CitySpeedFactor = 0.9;

    private const double MphToMetersPerSecond = 0.44704;

    // Gold / Silver / Bronze as a multiple of the platinum time
    // (same spread as the original captured targets 95776 / 101373 / 107599 / 116333).
    private const double GoldTimeRatio = 1.0584;
    private const double SilverTimeRatio = 1.1234;
    private const double BronzeTimeRatio = 1.2146;

    // Drift is scored in points (higher is better); obj_drift_score uses 50k..200k thresholds.
    private const int DriftPlatinumMin = 150_000;
    private const int DriftPlatinumMax = 200_000;
    private const double DriftGoldRatio = 0.80;
    private const double DriftSilverRatio = 0.62;
    private const double DriftBronzeRatio = 0.45;

    private sealed record ChallengeTargets(bool HigherIsBetter, long PersonalBest, long Platinum, long Gold, long Silver, long Bronze);

    private static ChallengeTargets GenerateTargets(
        GridAutosportRaceType raceType,
        GridAutosportVehicleClassID vehicleClass,
        GridAutosportTrackModel trackModel)
    {
        if (raceType == GridAutosportRaceType.Drift)
        {
            long platinum = RoundTo(Rnd.Next(DriftPlatinumMin, DriftPlatinumMax + 1), 100);
            long bronze = RoundTo(platinum * DriftBronzeRatio, 100);
            return new ChallengeTargets(
                true,
                RandomPersonalBest(bronze, platinum),
                platinum,
                RoundTo(platinum * DriftGoldRatio, 100),
                RoundTo(platinum * DriftSilverRatio, 100),
                bronze);
        }

        // Race: total time over RaceLaps. Time Attack / Endurance: best lap.
        // Sprint routes (Sprint DLC) are point-to-point: always one full run, whatever the race type.
        bool pointToPoint = RaceTypeToTracks[GridAutosportRaceType.Sprint].Contains(trackModel);
        int laps = raceType == GridAutosportRaceType.Race && !pointToPoint ? RaceLaps : 1;

        var (minMph, maxMph) = VehicleClassAverageMph[vehicleClass];
        double mph = minMph + (maxMph - minMph) * PlatinumSpeedFraction;
        if (TrackModelIsCity[trackModel])
            mph *= CitySpeedFactor;

        double lapSeconds = TrackModelLength[trackModel] / (mph * MphToMetersPerSecond);
        double jitter = 0.97 + Rnd.NextDouble() * 0.06; // ±3% so equal setups don't get identical targets
        long platinumMs = (long)Math.Round(lapSeconds * laps * jitter * 1000);
        long bronzeMs = (long)Math.Round(platinumMs * BronzeTimeRatio);

        return new ChallengeTargets(
            false,
            RandomPersonalBest(platinumMs, bronzeMs),
            platinumMs,
            (long)Math.Round(platinumMs * GoldTimeRatio),
            (long)Math.Round(platinumMs * SilverTimeRatio),
            bronzeMs);
    }

    // Half the time no personal best (0), otherwise something between the two given bounds.
    private static long RandomPersonalBest(long low, long high) =>
        Rnd.Next(2) == 0 ? 0 : low + (long)(Rnd.NextDouble() * (high - low));

    private static long RoundTo(double value, int step) => (long)Math.Round(value / step) * step;

    // A class of the discipline that has at least one vehicle and one playable race type.
    private static GridAutosportVehicleClassID PickVehicleClass(GridAutosportDisciplineID discipline)
    {
        var candidates = DisciplineToVehicleClasses[discipline]
            .Where(c => VehicleClassToVehicles.ContainsKey(c)
                        && VehicleClassToRaceTypes.TryGetValue(c, out var types)
                        && types.Any(t => RaceTypeToTracks.ContainsKey(t)))
            .ToArray();

        return Pick(candidates);
    }

    // A track valid for the race type, preferring tracks not already used in this set.
    private static GridAutosportTrackModel PickTrack(GridAutosportRaceType raceType, HashSet<GridAutosportTrackModel> usedTracks)
    {
        var valid = RaceTypeToTracks[raceType].Where(TrackConditions.ContainsKey).ToArray();
        var unused = valid.Where(t => !usedTracks.Contains(t)).ToArray();
        return Pick(unused.Length > 0 ? unused : valid);
    }

    private static T Pick<T>(IReadOnlyList<T> items) => items[Rnd.Next(items.Count)];
}
