using System;
using System.Collections.Generic;
using System.Linq;
using static GridAutosportData;

public static class GridAutosportRandomizer
{
    // Seeded per generation so the same event id always produces the same races.
    [ThreadStatic] private static Random? _rnd;
    private static Random Rnd => _rnd ??= new Random();

    /// <summary>Chance that a race uses a single-make cup class instead of a main class of its discipline.</summary>
    private const double CupClassChance = 0.15;

    public sealed record ChallengeEvent(long EventId, DateTimeOffset Expires, GridAutoSportChallengeRace[] Races);

    private static long _lastEventId;

    /// <summary>
    /// A brand new event, generated every time the game loads the challenge list.
    /// Event id = Unix seconds (strictly increasing), race ids = event id * 10 + index, so races
    /// from different loads never share an id.
    /// </summary>
    public static ChallengeEvent GenerateNewEvent(DateTimeOffset now, int totalRaces = 6)
    {
        long eventId;
        long last;
        do
        {
            last = Interlocked.Read(ref _lastEventId);
            eventId = Math.Max(now.ToUnixTimeSeconds(), last + 1);
        }
        while (Interlocked.CompareExchange(ref _lastEventId, eventId, last) != last);

        return new ChallengeEvent(eventId, now.AddDays(7), GenerateRandomRaces(totalRaces, firstRaceId: eventId * 10));
    }

    // Track model -> available conditions, from LocationToTracksMap.
    private static readonly IReadOnlyDictionary<GridAutosportTrackModel, List<GridAutosportTrackModelConditions>> TrackConditions =
        LocationToTracksMap.Values
            .SelectMany(tracks => tracks)
            .Where(t => t.Conditions.Count > 0)
            .ToDictionary(t => t.Track, t => t.Conditions);

    // Party mode: the original server always used Checkpoint with a party class (racing_style 8).
    // Eliminator and Demolition Derby are left out: no real data to build their targets from.
    private static readonly GridAutosportVehicleClassID[] PartyCheckpointClasses = VehicleClassToRaceTypes
        .Where(entry => !VehicleClassToDiscipline.ContainsKey(entry.Key)
                        && entry.Value.Contains(GridAutosportRaceType.Checkpoint))
        .Select(entry => entry.Key)
        .OrderBy(c => (int)c)
        .ToArray();

    public static GridAutoSportChallengeRace[] GenerateRandomRaces(int totalRaces = 6, int? seed = null, long firstRaceId = 1)
    {
        // One race per discipline (the five main ones + Party), then fill the remaining slots randomly.
        var allDisciplines = DisciplineToVehicleClasses.Keys.Append(GridAutosportDisciplineID.Party).ToArray();
        if (totalRaces < allDisciplines.Length)
            throw new ArgumentException($"Sono necessarie almeno {allDisciplines.Length} gare (una per disciplina).");

        _rnd = seed is int s ? new Random(s) : new Random();

        var disciplines = allDisciplines.ToList();
        while (disciplines.Count < totalRaces)
            disciplines.Add(Pick(allDisciplines));
        // Always Touring, Endurance, Openwheel, Tuner, Street, Party (enum order).
        disciplines = disciplines.OrderBy(d => (int)d).ToList();

        var usedTracks = new HashSet<GridAutosportTrackModel>();
        var generatedRaces = new List<GridAutoSportChallengeRace>();

        for (int i = 0; i < totalRaces; i++)
        {
            var discipline = disciplines[i];
            bool party = discipline == GridAutosportDisciplineID.Party;
            var vehicleClass = party ? Pick(PartyCheckpointClasses) : PickVehicleClass(discipline);
            var raceType = party
                ? GridAutosportRaceType.Checkpoint
                : Pick(VehicleClassToRaceTypes[vehicleClass].Where(RaceTypeToTracks.ContainsKey).ToArray());
            var trackModel = PickTrack(raceType, usedTracks);
            var condition = Pick(TrackConditions[trackModel]);
            usedTracks.Add(trackModel);

            bool specialRace = discipline == GridAutosportDisciplineID.Endurance;
            var targets = GenerateTargets(raceType, vehicleClass, trackModel);

            // The original server sent the class tier (vehicle_class.tier_id) as VehicleTierId.
            generatedRaces.Add(new GridAutoSportChallengeRace(
                firstRaceId + i, targets.HigherIsBetter, discipline, condition, raceType, 0,
                VehicleClassToTier[vehicleClass], vehicleClass, 0, 0,
                specialRace, 1, -1, targets.PersonalBest,
                targets.Platinum, targets.Gold, targets.Silver, targets.Bronze
            ));
        }

        return generatedRaces.ToArray();
    }

    // ---- Medal targets ----------------------------------------------------------------------
    // Calibrated on the original RaceNet events (racenet_logs_final, events 798/799).
    // Times are in milliseconds. Real examples (platinum):
    //   Time Attack  CatCTouring    RedBullRingRoute1  95.776 s   NsxCup  CallahanOval1  93.263 s
    //                Hypercar       BarcelonaRoute0   100.559 s   GT2     Hockenheim2    79.793 s
    //   Race         FormulaA       AustinRoute0      315.700 s   Coupe   Washington5   182.900 s
    //   Endurance    GT1            YasMarinaRoute1    18601 (higher is better)

    /// <summary>Laps counted in the target time for a Race event (RaceDuration = 0 uses the default).</summary>
    private const int RaceLaps = 3;

    // Platinum average speed (mph) = SpeedBaseMph + SpeedSlope * class mid speed (avarage_race_mph min/max).
    // Linear fit on the real Time Attack targets: real speeds sit around 95..125 mph whatever the class.
    private const double SpeedBaseMph = 58.7;
    private const double SpeedSlope = 0.44;

    /// <summary>Street circuits / city routes are slower than the class average.</summary>
    private const double CitySpeedFactor = 0.9;

    /// <summary>Race targets include the standing start and traffic: slower than a clean lap.</summary>
    private const double RaceTimeFactor = 1.08;

    private const double MphToMetersPerSecond = 0.44704;

    // Gold / Silver / Bronze as a multiple of the platinum time (averages of the real events).
    private const double LapGoldRatio = 1.055, LapSilverRatio = 1.121, LapBronzeRatio = 1.215;   // Time Attack, Sprint
    private const double RaceGoldRatio = 1.047, RaceSilverRatio = 1.079, RaceBronzeRatio = 1.128; // Race

    // Endurance (race type 23) is higher-is-better: distance-like score, ~ platinum speed x 390 s
    // (GT1 at YasMarinaRoute1 -> 18601, as on the original server). Medals below platinum as in that event.
    private const double EnduranceSeconds = 390;
    private const double EnduranceGoldRatio = 0.964, EnduranceSilverRatio = 0.934, EnduranceBronzeRatio = 0.8955;

    // Party Checkpoint (race type 22) is higher-is-better with the same distance-like score, ~ speed x 220 s:
    //   McLaren F1 party, MontTremblantRoute1 -> 11335 (gold 8104, silver 4829)
    //   Atom V8 party,    AlgarveRoute0       -> 10285 (gold 9592, silver 7652)
    // Gold / silver are the averages of the two events; bronze was cut off in the logs (extrapolated).
    private const double CheckpointSeconds = 220;
    private const double CheckpointGoldRatio = 0.824, CheckpointSilverRatio = 0.585, CheckpointBronzeRatio = 0.40;

    // Drift is scored in points (higher is better); obj_drift_score uses 50k..200k thresholds. No real data.
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
            return HigherIsBetterTargets(platinum, DriftGoldRatio, DriftSilverRatio, DriftBronzeRatio, step: 100);
        }

        var (minMph, maxMph) = VehicleClassAverageMph[vehicleClass];
        double mph = SpeedBaseMph + SpeedSlope * (minMph + maxMph) / 2.0;
        if (TrackModelIsCity[trackModel])
            mph *= CitySpeedFactor;
        double metersPerSecond = mph * MphToMetersPerSecond;
        double jitter = 0.97 + Rnd.NextDouble() * 0.06; // ±3% so equal setups don't get identical targets

        if (raceType == GridAutosportRaceType.Checkpoint)
        {
            long platinum = (long)Math.Round(metersPerSecond * CheckpointSeconds * jitter);
            return HigherIsBetterTargets(platinum, CheckpointGoldRatio, CheckpointSilverRatio, CheckpointBronzeRatio, step: 1);
        }

        if (raceType == GridAutosportRaceType.Endurance)
        {
            long platinum = (long)Math.Round(metersPerSecond * EnduranceSeconds * jitter);
            return HigherIsBetterTargets(platinum, EnduranceGoldRatio, EnduranceSilverRatio, EnduranceBronzeRatio, step: 1);
        }

        // Race: total time over RaceLaps. Time Attack: best lap. Sprint: one full run.
        // Sprint routes (Sprint DLC) are point-to-point: always one run, whatever the race type.
        bool pointToPoint = RaceTypeToTracks[GridAutosportRaceType.Sprint].Contains(trackModel);
        bool isRace = raceType == GridAutosportRaceType.Race;
        int laps = isRace && !pointToPoint ? RaceLaps : 1;

        double seconds = TrackModelLength[trackModel] * laps / metersPerSecond * (isRace ? RaceTimeFactor : 1.0);
        long platinumMs = (long)Math.Round(seconds * jitter * 1000);

        var (gold, silver, bronze) = isRace
            ? (RaceGoldRatio, RaceSilverRatio, RaceBronzeRatio)
            : (LapGoldRatio, LapSilverRatio, LapBronzeRatio);
        long bronzeMs = (long)Math.Round(platinumMs * bronze);

        return new ChallengeTargets(
            false,
            RandomPersonalBest(platinumMs, bronzeMs),
            platinumMs,
            (long)Math.Round(platinumMs * gold),
            (long)Math.Round(platinumMs * silver),
            bronzeMs);
    }

    private static ChallengeTargets HigherIsBetterTargets(long platinum, double gold, double silver, double bronze, int step)
    {
        long bronzeValue = RoundTo(platinum * bronze, step);
        return new ChallengeTargets(
            true,
            RandomPersonalBest(bronzeValue, platinum),
            platinum,
            RoundTo(platinum * gold, step),
            RoundTo(platinum * silver, step),
            bronzeValue);
    }

    // Half the time no personal best (0), otherwise something between the two given bounds.
    private static long RandomPersonalBest(long low, long high) =>
        Rnd.Next(2) == 0 ? 0 : low + (long)(Rnd.NextDouble() * (high - low));

    private static long RoundTo(double value, int step) => (long)Math.Round(value / step) * step;

    // A class of the discipline that has at least one vehicle and one playable race type.
    // Main classes are picked most of the time; single-make cups only with CupClassChance.
    private static GridAutosportVehicleClassID PickVehicleClass(GridAutosportDisciplineID discipline)
    {
        var candidates = DisciplineToVehicleClasses[discipline]
            .Where(c => VehicleClassToVehicles.ContainsKey(c)
                        && VehicleClassToRaceTypes.TryGetValue(c, out var types)
                        && types.Any(t => RaceTypeToTracks.ContainsKey(t)))
            .ToArray();

        var cups = candidates.Where(CupVehicleClasses.Contains).ToArray();
        var main = candidates.Where(c => !CupVehicleClasses.Contains(c)).ToArray();

        if (main.Length == 0)
            return Pick(cups);
        if (cups.Length == 0)
            return Pick(main);
        return Rnd.NextDouble() < CupClassChance ? Pick(cups) : Pick(main);
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
