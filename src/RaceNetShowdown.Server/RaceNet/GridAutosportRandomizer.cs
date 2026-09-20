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
        GridAutosportTrackModel.OkutamaP2pRoute5,
        GridAutosportTrackModel.CaliforniaRoute0,
        GridAutosportTrackModel.CaliforniaRoute1,
        GridAutosportTrackModel.CaliforniaRoute2,
        GridAutosportTrackModel.CaliforniaRoute3,
        GridAutosportTrackModel.CaliforniaRoute4,
        GridAutosportTrackModel.CaliforniaRoute5
    };

    private static readonly HashSet<GridAutosportTrackModel> DriftAllowedTracks = new()
    {
        GridAutosportTrackModel.CallahanOvalRoute5,
        GridAutosportTrackModel.BrandsHatchRoute5
    };

    public static GridAutoSportChallengeRace[] GenerateRandomRaces(int totalRaces = 6)
    {
        if (totalRaces < 5)
            throw new ArgumentException("Sono necessarie almeno 5 gare.");

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

        disciplines = disciplines.OrderBy(_ => Rnd.Next()).ToList();

        var allConditions = Enum.GetValues(typeof(GridAutosportTrackModelConditions))
                                .Cast<GridAutosportTrackModelConditions>()
                                .ToArray();

        var generatedRaces = new List<GridAutoSportChallengeRace>();

        for (int i = 0; i < totalRaces; i++)
        {
            int raceId = i + 1;
            var discipline = disciplines[i];
            var raceType = GetRandomRaceType(discipline);

            GridAutosportTrackModelConditions condition;
            GridAutosportTrackModel trackModel;

            do
            {
                condition = allConditions[Rnd.Next(allConditions.Length)];
                trackModel = GridAutosportData.GetTrackModel(condition);
            }
            while (!IsTrackValidForRace(discipline, raceType, trackModel));

            var vehicleClass = GetRandomVehicleClass(discipline, raceType);

            bool isEnduranceParam = (discipline == GridAutosportDisciplineID.Endurance);
            int scoreParam1 = (Rnd.Next(2) == 0) ? 0 : 98112;

            generatedRaces.Add(new GridAutoSportChallengeRace(
                raceId, false, discipline, condition, raceType, 0, -1, vehicleClass, 0, 0,
                isEnduranceParam, 1, -1, scoreParam1, 95776, 101373, 107599, 116333
            ));
        }

        return generatedRaces.ToArray();
    }

    private static bool IsTrackValidForRace(
        GridAutosportDisciplineID discipline,
        GridAutosportRaceType raceType,
        GridAutosportTrackModel trackModel)
    {
        if (raceType == GridAutosportRaceType.Drift)
        {
            return DriftAllowedTracks.Contains(trackModel);
        }

        if (DriftAllowedTracks.Contains(trackModel))
        {
            return false;
        }

        if (raceType == GridAutosportRaceType.Sprint)
        {
            return SprintAllowedTracks.Contains(trackModel);
        }

        if (discipline == GridAutosportDisciplineID.Endurance || raceType == GridAutosportRaceType.Endurance)
        {
            return !SprintAllowedTracks.Contains(trackModel);
        }

        return true;
    }

    private static GridAutosportRaceType GetRandomRaceType(GridAutosportDisciplineID discipline)
    {
        var validTypes = discipline switch
        {
            GridAutosportDisciplineID.Endurance =>
                new[] { GridAutosportRaceType.Endurance },

            GridAutosportDisciplineID.Openwheel =>
                new[] { GridAutosportRaceType.Race, GridAutosportRaceType.TimeAttack },

            GridAutosportDisciplineID.Tuner =>
                new[] { GridAutosportRaceType.TimeAttack, GridAutosportRaceType.Drift, GridAutosportRaceType.Race },

            GridAutosportDisciplineID.Street =>
                new[] { GridAutosportRaceType.Race, GridAutosportRaceType.Sprint },

            _ =>
                new[] { GridAutosportRaceType.Race, GridAutosportRaceType.Sprint }
        };

        return validTypes[Rnd.Next(validTypes.Length)];
    }

    private static GridAutosportVehicleClassID GetRandomVehicleClass(
        GridAutosportDisciplineID discipline,
        GridAutosportRaceType raceType)
    {
        if (raceType == GridAutosportRaceType.Drift)
        {
            GridAutosportVehicleClassID[] driftClasses = [GridAutosportVehicleClassID.C1Drift, GridAutosportVehicleClassID.C2Drift];
            return driftClasses[Rnd.Next(driftClasses.Length)];
        }

        var validClasses = discipline switch
        {
            GridAutosportDisciplineID.Touring =>
                new[] { GridAutosportVehicleClassID.CatATouring, GridAutosportVehicleClassID.CatBTouring, GridAutosportVehicleClassID.CatCTouring },

            GridAutosportDisciplineID.Endurance =>
                new[] { GridAutosportVehicleClassID.EnduranceGtGroup1, GridAutosportVehicleClassID.EnduranceGtGroup2, GridAutosportVehicleClassID.EnduranceGtUltimate },

            GridAutosportDisciplineID.Openwheel =>
                new[] { GridAutosportVehicleClassID.FormulaA, GridAutosportVehicleClassID.FormulaB, GridAutosportVehicleClassID.FormulaC },

            GridAutosportDisciplineID.Tuner =>
                new[] { GridAutosportVehicleClassID.Modified, GridAutosportVehicleClassID.SuperModified, GridAutosportVehicleClassID.Jdm },

            GridAutosportDisciplineID.Street =>
                new[] { GridAutosportVehicleClassID.HotHatch, GridAutosportVehicleClassID.Supercar, GridAutosportVehicleClassID.Hypercar },

            _ =>
                new[] { GridAutosportVehicleClassID.CatATouring }
        };

        return validClasses[Rnd.Next(validClasses.Length)];
    }
}