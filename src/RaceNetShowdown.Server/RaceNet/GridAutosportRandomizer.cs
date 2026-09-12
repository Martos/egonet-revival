using System;
using System.Collections.Generic;
using System.Linq;
using static GridAutosportData;

public static class GridAutosportRandomizer
{
    private static readonly Random Rnd = new Random();

    private static readonly HashSet<GridAutosportTrackModel> SprintAllowedTracks = new()
    {
        GridAutosportTrackModel.CoteDAzurRoute0,
        GridAutosportTrackModel.CoteDAzurRoute1,
        GridAutosportTrackModel.CoteDAzurRoute2,
        GridAutosportTrackModel.CoteDAzurRoute3,
        GridAutosportTrackModel.CoteDAzurRoute4,
        GridAutosportTrackModel.CoteDAzurRoute5,
        GridAutosportTrackModel.HongKongRoute0,
        GridAutosportTrackModel.HongKongRoute1,
        GridAutosportTrackModel.HongKongRoute2,
        GridAutosportTrackModel.HongKongRoute3,
        GridAutosportTrackModel.HongKongRoute4,
        GridAutosportTrackModel.HongKongRoute5,
        GridAutosportTrackModel.OkutamaP2pRoute0,
        GridAutosportTrackModel.OkutamaP2pRoute1,
        GridAutosportTrackModel.OkutamaP2pRoute2,
        GridAutosportTrackModel.OkutamaP2pRoute3,
        GridAutosportTrackModel.OkutamaP2pRoute4,
        GridAutosportTrackModel.OkutamaP2pRoute5
    };

    public static GridAutoSportChallengeRace[] GenerateRandomRaces(int totalRaces = 6)
    {
        if (totalRaces < 5)
            throw new ArgumentException("Need 5 races.");

        var disciplines = new List<GridAutosportDisciplineID>
        {
            GridAutosportDisciplineID.Touring,
            GridAutosportDisciplineID.Endurance,
            GridAutosportDisciplineID.Street,
            GridAutosportDisciplineID.Tuner,
            GridAutosportDisciplineID.Openwheel
        };

        var allDisciplines = Enum.GetValues(typeof(GridAutosportDisciplineID))
                                 .Cast<GridAutosportDisciplineID>()
                                 .ToArray();

        while (disciplines.Count < totalRaces)
        {
            disciplines.Add(allDisciplines[Rnd.Next(allDisciplines.Length)]);
        }

        disciplines = disciplines.OrderBy(x => Rnd.Next()).ToList();

        var allConditions = Enum.GetValues(typeof(GridAutosportTrackModelConditions))
                                .Cast<GridAutosportTrackModelConditions>()
                                .ToArray();

        var generatedRaces = new List<GridAutoSportChallengeRace>();

        for (int i = 0; i < totalRaces; i++)
        {
            int raceId = i + 1;
            var discipline = disciplines[i];
            var condition = allConditions[Rnd.Next(allConditions.Length)];
            var trackModel = GridAutosportData.GetTrackModel(condition);
            var raceType = GetRandomRaceType(discipline, trackModel);
            var vehicleClass = GetRandomVehicleClass(discipline);

            bool isEnduranceParam = (discipline == GridAutosportDisciplineID.Endurance);
            int scoreParam1 = (Rnd.Next(2) == 0) ? 0 : 98112;

            generatedRaces.Add(new GridAutoSportChallengeRace(
                raceId, false, discipline, condition, raceType, 0, -1, vehicleClass, 0, 0,
                isEnduranceParam, 1, -1, scoreParam1, 95776, 101373, 107599, 116333
            ));
        }

        return generatedRaces.ToArray();
    }

    private static GridAutosportRaceType GetRandomRaceType(
        GridAutosportDisciplineID discipline,
        GridAutosportTrackModel trackModel)
    {
        bool canSprint = SprintAllowedTracks.Contains(trackModel);

        GridAutosportRaceType[] gridAutosportRaceTypes = [GridAutosportRaceType.Race];
        var validTypes = discipline switch
        {
            GridAutosportDisciplineID.Endurance =>
                [GridAutosportRaceType.Endurance],

            GridAutosportDisciplineID.Openwheel =>
                [GridAutosportRaceType.Race, GridAutosportRaceType.TimeAttack],

            GridAutosportDisciplineID.Tuner =>
                [GridAutosportRaceType.TimeAttack, GridAutosportRaceType.Drift, GridAutosportRaceType.Race],

            GridAutosportDisciplineID.Street => canSprint
                ? [GridAutosportRaceType.Race, GridAutosportRaceType.Sprint]
                : [GridAutosportRaceType.Race],

            // Fallback
            _ => canSprint
                ? [GridAutosportRaceType.Race, GridAutosportRaceType.Sprint]
                : gridAutosportRaceTypes
        };

        return validTypes[Rnd.Next(validTypes.Length)];
    }

    private static GridAutosportVehicleClassID GetRandomVehicleClass(GridAutosportDisciplineID discipline)
    {
        GridAutosportVehicleClassID[] gridAutosportVehicleClassIDs = [GridAutosportVehicleClassID.HotHatch, GridAutosportVehicleClassID.Supercar, GridAutosportVehicleClassID.Hypercar];
        var validClasses = discipline switch
        {
            GridAutosportDisciplineID.Touring => [GridAutosportVehicleClassID.CatATouring, GridAutosportVehicleClassID.CatBTouring, GridAutosportVehicleClassID.CatCTouring],
            GridAutosportDisciplineID.Endurance => [GridAutosportVehicleClassID.EnduranceGtGroup1, GridAutosportVehicleClassID.EnduranceGtGroup2, GridAutosportVehicleClassID.EnduranceGtUltimate],
            GridAutosportDisciplineID.Openwheel => [GridAutosportVehicleClassID.FormulaA, GridAutosportVehicleClassID.FormulaB, GridAutosportVehicleClassID.FormulaC],
            GridAutosportDisciplineID.Tuner => [GridAutosportVehicleClassID.Modified, GridAutosportVehicleClassID.SuperModified, GridAutosportVehicleClassID.Jdm],
            GridAutosportDisciplineID.Street => gridAutosportVehicleClassIDs,
            _ => [GridAutosportVehicleClassID.CatATouring]
        };
        return validClasses[Rnd.Next(validClasses.Length)];
    }
}