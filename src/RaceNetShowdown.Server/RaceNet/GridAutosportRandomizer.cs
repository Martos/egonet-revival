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
            int personalBest = (Rnd.Next(2) == 0) ? 0 : 98112;

            generatedRaces.Add(new GridAutoSportChallengeRace(
                i + 1, false, discipline, condition, raceType, 0, -1, vehicleClass, 0, 0,
                specialRace, 1, -1, personalBest, 95776, 101373, 107599, 116333
            ));
        }

        return generatedRaces.ToArray();
    }

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
