using RaceNetShowdown.Server.Data;
using RaceNetShowdown.Server.Infrastructure;
using System.Security.Cryptography;
using System.Text;

namespace RaceNetShowdown.Server.RaceNet;

internal static class Grid2EgoNetPayloads
{
    private const string HtmlContentType = "text/html";
    private const string EgoNetContentType = "application/egonet-stream";

    private static readonly Grid2Presence[] RivalPresences =
    [
        new(76_561_198_081_105_540, "Heatray", 2_536_759, 2),
        new(76_561_197_998_609_659, "MMMMMMMMM", 551_744, 0),
        new(76_561_198_195_857_139, "sami_1_3_5", 2_473_109, 1)
    ];

    private static readonly Grid2GlobalRace[] CurrentGlobalRaces =
    [
        new(6_282, false, 31, 355, 0, 554, 24, 7, 3, -1, -1, 0, true, 1, 77_215_000, 65_763),
        new(6_283, true, 33, 408, 0, 361, 7, 79_102, 3, -1, -1, 0, false, 2, 662_890_000, 367),
        new(6_284, true, 27, 333, 0, 367, 17, 6, 5, -1, -1, 0, false, 3, 88_100_000, 131_331),
        new(6_285, true, 26, 326, 0, 517, 22, 7, 5, -1, -1, 0, false, 4, 12_498_828, 390),
        new(6_286, false, 32, 362, 0, 409, 24, 4, 3, -1, -1, 0, false, 5, 77_739_000, 65_763),
        new(6_287, true, 32, 359, 0, 509, 22, 7, 2, -1, -1, 0, false, 6, 14_250_000, 294),
        new(6_288, true, 31, 357, 0, 327, 17, 6, 2, -1, -1, 0, false, 7, 91_400_000, 131_331),
        new(6_289, false, 31, 357, 0, 327, 24, 5, 4, -1, -1, 0, false, 8, 82_020_000, 367),
        new(6_290, true, 33, 408, 0, 361, 7, 83_000, 4, -1, -1, 0, false, 9, 280_000_000, 390)
    ];

    private static readonly GridAutoSportChallengeRace[] GridAutosportChallengeRaces = [
        new(1, false, GridAutosportDisciplineID.Touring, GridAutosportLocation.RedBullRing, GridAutosportTrackModel.RedBullRingRoute1, 0, GridAutosportTrackModelConditions.RedBullRingRoute1Day, GridAutosportRaceType.TimeAttack, 0, -1, GridAutosportVehicleClassID.CatATouring, 0, 0, false, 1, -1, 98112, 95776, 101373, 107599, 116333),
        new(2, false, GridAutosportDisciplineID.Endurance, GridAutosportLocation.RedBullRing, GridAutosportTrackModel.RedBullRingRoute1, 0, GridAutosportTrackModelConditions.RedBullRingRoute1Day, GridAutosportRaceType.Endurance, 0, -1, GridAutosportVehicleClassID.EnduranceGtGroup1, 0, 0, true, 1, -1, 98112, 95776, 101373, 107599, 116333),
        new(3, false, GridAutosportDisciplineID.Openwheel, GridAutosportLocation.RedBullRing, GridAutosportTrackModel.RedBullRingRoute1, 0, GridAutosportTrackModelConditions.RedBullRingRoute1Day, GridAutosportRaceType.TimeAttack, 0, -1, GridAutosportVehicleClassID.FormulaA, 0, 0, false, 1, -1, 98112, 95776, 101373, 107599, 116333),
        new(4, false, GridAutosportDisciplineID.Street, GridAutosportLocation.SanFrancisco, GridAutosportTrackModel.SanFranciscoRoute0, 0, GridAutosportTrackModelConditions.SanFranciscoRoute0Day, GridAutosportRaceType.TimeAttack, 0, -1, GridAutosportVehicleClassID.HotHatch, 0, 0, false, 1, -1, 98112, 95776, 101373, 107599, 116333),
        new(5, false, GridAutosportDisciplineID.Tuner, GridAutosportLocation.RedBullRing, GridAutosportTrackModel.RedBullRingRoute1, 0, GridAutosportTrackModelConditions.RedBullRingRoute1Day, GridAutosportRaceType.TimeAttack, 0, -1, GridAutosportVehicleClassID.Modified, 0, 0, false, 1, -1, 98112, 95776, 101373, 107599, 116333),
    ];

    private static readonly Grid2GlobalRace[] PreviousGlobalRaces =
    [
        new(6_264, true, 25, 322, 0, 319, 22, 6, 4, -1, -1, 0, false, -1, 10_000_000, 390),
        new(6_265, false, 31, 355, 0, 554, 24, 7, 3, -1, -1, 0, false, -1, 78_000_000, 65_763),
        new(6_266, true, 32, 359, 0, 509, 22, 7, 2, -1, -1, 0, false, -1, 12_000_000, 294)
    ];

    public static RaceNetResponse? TryBuild(
        string functionName,
        CapturedBody body,
        RaceNetSessionInfo? session,
        IReadOnlyDictionary<string, string> headers)
    {
        return functionName switch
        {
            "LoginService.Login" => Html(BuildLogin(body, session), headers),
            "LoginService.Tick" => Empty(headers),
            "GameData.Get" => Html(BuildGameData(), headers),
            "LanguageService.FetchLanguageData" => Html(BuildLanguageData(), headers),
            "OnlineSkill.GetOnlineSkill" => Html(BuildOnlineSkill(), headers),
            "OnlineSkill.UpdateOnlineSkill" => Empty(headers),
            "RaceNet.CheckAccount" => Html(BuildCheckAccount(), headers),
            "RaceNet.CheckAccountLinked" => Html(BuildCheckAccountLinked(), headers),
            "RaceNet.CreateAccount" => Html(BuildAccountProfile(body, session), headers),
            "RaceNet.GetContentMask" => Html(EgoNetBinary.Dictionary(), headers),
            "RaceNet.GetNewsFeed" => Html(BuildNewsFeed(), headers),
            "RaceNet.GetTermsAndConditions" => Html(BuildTermsAndConditions(), headers),
            "RaceNet.SignIn" => Html(BuildAccountProfile(body, session), headers),
            "RaceNet.UnlinkAccount" => Empty(headers),
            "RaceNet.ValidateDateOfBirth" => Html(BuildValidation(), headers),
            "RaceNet.ValidateEmail" => Html(BuildValidation(), headers),
            "RaceNet.ValidatePassword" => Html(BuildValidation(), headers),
            "RaceNet.ValidateSocialLinks" => Html(BuildValidation(), headers),
            "RaceNet.ValidateUsername" => Html(BuildValidation(), headers),
            "RaceNetGlobalDomination.GetEvent" => Html(BuildCurrentGlobalDomination(), headers),
            "RaceNetGlobalDomination.GetPreviousEvent" => Html(BuildPreviousGlobalDomination(), headers),
            "RaceNetGlobalDomination.PostScore" => Empty(headers),
            "RaceNetRivals.BlockRival" => Empty(headers),
            "RaceNetRivals.GetRivals" => Html(BuildRivals(), headers),
            "RaceNetRivals.PostRivalResults" => Empty(headers),
            "Rivals.GetRivalsSessionData" => Html(BuildRivalsSessionData(), headers),
            "Rivals.UpdateRivalsSessionData" => Empty(headers),
            "DataMiningHelper.GetRaceId" => Html(BuildRaceId(), headers),
            "DataMining.EndEvent" => Empty(headers),
            "DataMining.EndRace" => Empty(headers),
            "DataMining.PcHardware" => Empty(headers),
            "DataMining.Profile" => Empty(headers),
            "DataMining.StartEvent" => Empty(headers),
            "GhostCar.Download" => Html(BuildGhostDownload(), headers),
            "GhostCar.Upload" => Empty(headers),
            "Statistics.SubmitRaceEndedHeatMapData" => Empty(headers),
            "Texture.FetchTexture" => Empty(headers),
            "Video.EnterQueue" => Empty(headers),
            "Video.PollQueue" => Empty(headers),
            "Video.Upload" => Empty(headers),
            "VipPassTrial.ProceedWithTrial" => Empty(headers),
            "RaceNetDisciplineChallenge.GetEvent" => Html(BuildChallengeRaces(), headers),
            "RaceNetDisciplineChallenge.GetRanks" => Empty(headers),
            _ => null
        };
    }

    private static RaceNetResponse Html(byte[] body, IReadOnlyDictionary<string, string> headers)
    {
        return new RaceNetResponse(HtmlContentType, body, headers);
    }

    private static RaceNetResponse Empty(IReadOnlyDictionary<string, string> headers)
    {
        return new RaceNetResponse(EgoNetContentType, [], headers);
    }

    private static byte[] BuildLogin(CapturedBody body, RaceNetSessionInfo? session)
    {
        var name = ReadAccountName(body, session);
        var steamId = BuildStableSteamId(name);
        var principalId = session?.PlayerProfileId > 0
            ? session.PlayerProfileId
            : 752_828;

        return EgoNetBinary.Dictionary(
            EgoNetBinary.Si64("PrincipalId", principalId),
            EgoNetBinary.Dstr("Name", name),
            EgoNetBinary.Ui64("XUID", 0),
            EgoNetBinary.Ui64("SteamId", steamId),
            EgoNetBinary.Ui64("SubjectId", 0));
    }

    private static byte[] BuildAccountProfile(CapturedBody body, RaceNetSessionInfo? session)
    {
        var name = ReadAccountName(body, session);
        var steamId = BuildStableSteamId(name);
        var principalId = session?.PlayerProfileId > 0
            ? session.PlayerProfileId
            : 752_828;

        return EgoNetBinary.Dictionary(
            EgoNetBinary.Si64("PrincipalId", principalId),
            EgoNetBinary.Dstr("Name", name),
            EgoNetBinary.Ui64("XUID", 0),
            EgoNetBinary.Ui64("SteamId", steamId),
            EgoNetBinary.Ui64("SubjectId", 0));
    }

    private static string ReadAccountName(CapturedBody body, RaceNetSessionInfo? session)
    {
        return EgoNetRequestParser.ReadTopLevelString(body, "Name") ??
            EgoNetRequestParser.ReadTopLevelString(body, "Username") ??
            session?.DisplayName ??
            "GRID 2 Player";
    }

    private static byte[] BuildGameData()
    {
        return EgoNetBinary.Dictionary(
            EgoNetBinary.Blob("DbPatch", [0]),
            EgoNetBinary.Si32("DbPatchSize", 0),
            EgoNetBinary.Vector("DlcPreview"));
    }

    private static byte[] BuildLanguageData()
    {
        return EgoNetBinary.Dictionary(
            EgoNetBinary.Vector("Translations"),
            EgoNetBinary.Si32("Version", 1));
    }

    private static byte[] BuildOnlineSkill()
    {
        return EgoNetBinary.Dictionary(
            EgoNetBinary.Dict(
                "SkillRating",
                EgoNetBinary.Fp64("Rating", 0),
                EgoNetBinary.Bool("IsValid", false)));
    }

    private static byte[] BuildCheckAccount()
    {
        return EgoNetBinary.Dictionary(
            EgoNetBinary.Bool("Exists", false),
            EgoNetBinary.Bool("PasswordExists", false));
    }

    private static byte[] BuildCheckAccountLinked()
    {
        return EgoNetBinary.Dictionary(
            EgoNetBinary.Bool("IsLinked", true));
    }

    private static byte[] BuildNewsFeed()
    {
        return EgoNetBinary.Dictionary(
            EgoNetBinary.Vector("News"));
    }

    private static byte[] BuildTermsAndConditions()
    {
        return EgoNetBinary.Dictionary(
            EgoNetBinary.Dstr("TermsConditions", string.Empty),
            EgoNetBinary.Dstr("PasswordText", string.Empty),
            EgoNetBinary.Dstr("UsernameText", string.Empty));
    }

    private static byte[] BuildValidation()
    {
        return EgoNetBinary.Dictionary(
            EgoNetBinary.Bool("IsValid", true),
            EgoNetBinary.Bool("Valid", true),
            EgoNetBinary.Bool("Available", true),
            EgoNetBinary.Bool("IsAvailable", true),
            EgoNetBinary.Bool("Exists", false),
            EgoNetBinary.Dstr("Error", string.Empty),
            EgoNetBinary.Dstr("Message", string.Empty));
    }

    private static byte[] BuildCurrentGlobalDomination()
    {
        return EgoNetBinary.Dictionary(
            EgoNetBinary.Si64("RaceNetId", 698),
            EgoNetBinary.Tutc("Expires", DateTimeOffset.UtcNow.AddDays(7)),
            EgoNetBinary.Vector(
                "Races",
                CurrentGlobalRaces.Select(BuildGlobalRace).ToArray()));
    }

    private static byte[] BuildPreviousGlobalDomination()
    {
        return EgoNetBinary.Dictionary(
            EgoNetBinary.Vector(
                "Races",
                PreviousGlobalRaces.Select(BuildGlobalRace).ToArray()));
    }
    private static byte[] BuildChallengeRaces()
    {
        return EgoNetBinary.Dictionary(
            EgoNetBinary.Si64("RaceNetId", 798),
            EgoNetBinary.Tutc("Expires", DateTimeOffset.UtcNow.AddDays(7)),
            EgoNetBinary.Vector(
                "Races",
                GridAutosportChallengeRaces.Select(BuildChallengeRace).ToArray()));
    }


    private static Action<BinaryWriter> BuildGlobalRace(Grid2GlobalRace race)
    {
        return EgoNetBinary.DictValue(
            EgoNetBinary.Si64("RaceNetId", race.RaceNetId),
            EgoNetBinary.Bool("HigherIsBetter", race.HigherIsBetter),
            EgoNetBinary.Si32("LocationId", race.LocationId),
            EgoNetBinary.Si32("TrackModelId", race.TrackModelId),
            EgoNetBinary.Si32("TrackModelDlcId", race.TrackModelDlcId),
            EgoNetBinary.Si32("ConditionsId", race.ConditionsId),
            EgoNetBinary.Si32("RaceTypeId", race.RaceTypeId),
            EgoNetBinary.Si64("RaceDuration", race.RaceDuration),
            EgoNetBinary.Si32("VehicleTierId", race.VehicleTierId),
            EgoNetBinary.Si32("VehicleClassId", race.VehicleClassId),
            EgoNetBinary.Si32("VehicleId", race.VehicleId),
            EgoNetBinary.Si32("VehicleDlcId", race.VehicleDlcId),
            EgoNetBinary.Bool("SpecialRace", race.SpecialRace),
            EgoNetBinary.Si64("GhostSlotId", race.GhostSlotId),
            EgoNetBinary.Vector(
                "Leaderboard",
                BuildGlobalLeaderboard(race)));
    }

    private static Action<BinaryWriter> BuildChallengeRace(GridAutoSportChallengeRace race)
    {
        return EgoNetBinary.DictValue(
            EgoNetBinary.Si64("RaceNetId", race.RaceNetId),
            EgoNetBinary.Bool("HigherIsBetter", race.HigherIsBetter),
            EgoNetBinary.Si32("DisciplineId", (int)race.DisciplineId),
            EgoNetBinary.Si32("LocationId", (int)race.LocationId),
            EgoNetBinary.Si32("TrackModelId", (int)race.TrackModelId),
            EgoNetBinary.Si32("TrackModelDlcId", race.TrackModelDlcId),
            EgoNetBinary.Si32("ConditionsId", (int)race.ConditionsId),
            EgoNetBinary.Si32("RaceTypeId", (int)race.RaceTypeId),
            EgoNetBinary.Si64("RaceDuration", race.RaceDuration),
            EgoNetBinary.Si32("VehicleTierId", race.VehicleTierId),
            EgoNetBinary.Si32("VehicleClassId", (int)race.VehicleClassId),
            EgoNetBinary.Si32("VehicleId", race.VehicleId),
            EgoNetBinary.Si32("VehicleDlcId", race.VehicleDlcId),
            EgoNetBinary.Bool("SpecialRace", race.SpecialRace),
            EgoNetBinary.Si64("GhostSlotId", race.GhostSlotId),
            EgoNetBinary.Si32("Rank", race.Rank),
            EgoNetBinary.Si64("PersonalBest", race.PersonalBest),
            EgoNetBinary.Si64("PlatinumTarget", race.PlatinumTarget),
            EgoNetBinary.Si64("GoldTarget", race.GoldTarget),
            EgoNetBinary.Si64("SilverTarget", race.SilverTarget),
            EgoNetBinary.Si64("BronzeTarget", race.BronzeTarget)
        );
    }

    private static Action<BinaryWriter>[] BuildGlobalLeaderboard(Grid2GlobalRace race)
    {
        if (race.PersonalBest <= 0)
        {
            return [];
        }

        return
        [
            EgoNetBinary.DictValue(
                BuildPresence("Presence", RivalPresences[0]),
                EgoNetBinary.Si64("PersonalBest", race.PersonalBest),
                EgoNetBinary.Si32("VehicleId", race.BestVehicleId))
        ];
    }

    private static byte[] BuildRivals()
    {
        return EgoNetBinary.Dictionary(
            EgoNetBinary.Vector(
                "RivalsList",
                RivalPresences.Select(BuildRival).ToArray()),
            EgoNetBinary.Tutc("NextRivalAlloc", DateTimeOffset.UtcNow.AddDays(7)));
    }

    private static Action<BinaryWriter> BuildRival(Grid2Presence rival)
    {
        return EgoNetBinary.DictValue(
            BuildPresence("Presence", rival),
            EgoNetBinary.Si64("PlatformId", checked((long)rival.SteamId)),
            EgoNetBinary.Si32("Type", rival.Type),
            EgoNetBinary.Bool("CanSeePresence", true),
            EgoNetBinary.Ui32("TotalXPWon", 0),
            EgoNetBinary.Ui32("RivalXPWon", 0));
    }

    private static EgoNetField BuildPresence(string name, Grid2Presence presence)
    {
        return EgoNetBinary.Dict(
            name,
            EgoNetBinary.Ui64("SteamId", presence.SteamId),
            EgoNetBinary.Dstr("Name", presence.Name),
            EgoNetBinary.Si64("EgonetId", presence.EgonetId));
    }

    private static byte[] BuildRivalsSessionData()
    {
        return EgoNetBinary.Dictionary(
            EgoNetBinary.Vector("Results"));
    }

    private static byte[] BuildRaceId()
    {
        return EgoNetBinary.Dictionary(
            EgoNetBinary.Si64("RaceId", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()));
    }

    private static byte[] BuildGhostDownload()
    {
        return EgoNetBinary.Dictionary(
            EgoNetBinary.Blob("GhostData", []));
    }

    private static ulong BuildStableSteamId(string name)
    {
        var normalized = name.Trim().ToLowerInvariant();
        var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(normalized));
        var hash = BitConverter.ToUInt64(hashBytes, 0);
        return 76_561_198_000_000_000UL + hash % 10_000_000_000UL;
    }

    private sealed record Grid2Presence(
        ulong SteamId,
        string Name,
        long EgonetId,
        int Type);

    private sealed record Grid2GlobalRace(
        long RaceNetId,
        bool HigherIsBetter,
        int LocationId,
        int TrackModelId,
        int TrackModelDlcId,
        int ConditionsId,
        int RaceTypeId,
        long RaceDuration,
        int VehicleTierId,
        int VehicleClassId,
        int VehicleId,
        int VehicleDlcId,
        bool SpecialRace,
        long GhostSlotId,
        long PersonalBest,
        int BestVehicleId);

    private sealed record GridAutoSportChallengeRace(
        long RaceNetId,
        bool HigherIsBetter,
        GridAutosportDisciplineID DisciplineId,
        GridAutosportLocation LocationId,
        GridAutosportTrackModel TrackModelId,
        int TrackModelDlcId,
        GridAutosportTrackModelConditions ConditionsId,
        GridAutosportRaceType RaceTypeId,
        long RaceDuration,
        int VehicleTierId,
        GridAutosportVehicleClassID VehicleClassId,
        int VehicleId,
        int VehicleDlcId,
        bool SpecialRace,
        long GhostSlotId,
        int Rank,
        long PersonalBest,
        long PlatinumTarget,
        long GoldTarget,
        long SilverTarget,
        long BronzeTarget
    );

    private enum GridAutosportDisciplineID
    {
        Touring = 2,
        Endurance = 3,
        Openwheel = 4,
        Tuner = 5,
        Street = 6
    }

    private enum GridAutosportVehicleClassID
    {
        SuperModified = 47,
        CatCTouring = 48,
        CuBmwE30M3 = 53,
        ClassicMuscle = 54,
        HotHatch = 56,
        Muscle = 59,
        Jdm = 60,
        CuLightweightCup = 63,
        Supercar = 64,
        C2Drift = 65,
        CatBTouring = 69,
        CatATouring = 70,
        SuperUtes = 71,
        SuperTourers = 72,
        CuClassicMiniCup = 73,
        ClassicTouringCar = 74,
        CuMiniCup = 75,
        EnduranceGtGroup2 = 76,
        EnduranceGtGroup1 = 77,
        EnduranceGtUltimate = 78,
        CuShelbyCup = 82,
        CuFordGt40Cup = 83,
        CuMazda787bCup = 84,
        FormulaC = 86,
        FormulaB = 87,
        FormulaA = 88,
        CuCaterhamLolaCup = 89,
        CuArielAtomV8Cup = 90,
        CuSuperLightweightCup = 91,
        Modified = 92,
        C1Drift = 93,
        CuNissanSkylineCup = 94,
        CuHondaNsxCup = 95,
        Coupe = 96,
        Performance = 97,
        Gt = 98,
        Hypercar = 99,
        CuLanciaDeltaCup = 100,
        CuAlfaRomeo4cCup = 101,
        CuMclarenF1Cup = 102,
        DemoDerby = 106,
        Muscle2 = 108,
        Jdm2 = 109,
        HotHatch2 = 110,
        Coupe2 = 111,
        ClassicMuscle2 = 112,
        CuLanciaDeltaCup2 = 113,
        Modified2 = 114,
        Performance2 = 115,
        Gt2 = 116,
        CuNissanSkylineCup2 = 117,
        CuBmwE30M32 = 118,
        CuAlfaRomeo4cCup2 = 119,
        SuperModified2 = 120,
        Supercar2 = 121,
        Hypercar2 = 122,
        CuHondaNsxCup2 = 123,
        CuMclarenF1Cup2 = 124,
        CuCaterhamLolaCup2 = 125,
        CuLightweightCup2 = 126,
        CuArielAtomV8Cup2 = 127,
        CuSuperLightweightCup2 = 128,
        CuClassicMiniCup2 = 129,
        CuMiniCup2 = 130,
        ClassicTouringCarCup = 131,
        CuMazda787bCup2 = 133,
        CuFordGt40Cup2 = 135,
        CuShelbyCup2 = 136
    }

    private enum GridAutosportRaceType
    {
        Race = 2,
        TimeAttack = 4,
        Drift = 7,
        Eliminator = 14,
        Overtake = 17,
        Checkpoint = 22,
        Endurance = 23,
        Drag = 32,
        TimeTrial = 34,
        DemoDerby = 35,
        Sprint = 38
    }
    private enum GridAutosportLocation
    {
        Various = 11,
        Barcelona = 25,
        Chicago = 26,
        Dubai = 27,
        Paris = 29,
        Algarve = 30,
        BrandsHatch = 31,
        Indianapolis = 32,
        YasMarina = 39,
        RedBullRing = 40,
        SanFrancisco = 41,
        Washington = 42,
        MontTremblant = 47,
        Austin = 49,
        CallahanOval = 50,
        Hockenheim = 51,
        Jarama = 52,
        OkutamaGp = 53,
        Sepang = 54,
        IstanbulPark = 55,
        SpaFrancorchamps = 56,
        Bathurst = 57,
        Detroit = 59
    }

    private enum GridAutosportTrackModel
    {
        Skipfe = 118,
        Skipfe183 = 183,
        Speedway = 184,
        BarcelonaRoute0 = 318,
        BarcelonaRoute1 = 320,
        BarcelonaRoute2 = 321,
        BarcelonaRoute3 = 322,
        BarcelonaRoute4 = 323,
        BarcelonaRoute5 = 324,
        ChicagoRoute0 = 325,
        ChicagoRoute1 = 326,
        ChicagoRoute2 = 327,
        ChicagoRoute3 = 328,
        ChicagoRoute4 = 329,
        ChicagoRoute5 = 330,
        DubaiRoute0 = 333,
        DubaiRoute1 = 334,
        DubaiRoute2 = 335,
        DubaiRoute3 = 336,
        DubaiRoute4 = 337,
        DubaiRoute5 = 338,
        ParisRoute0 = 345,
        ParisRoute1 = 346,
        ParisRoute2 = 347,
        ParisRoute3 = 348,
        ParisRoute4 = 349,
        ParisRoute5 = 350,
        AlgarveRoute0 = 351,
        AlgarveRoute1 = 352,
        AlgarveRoute2 = 353,
        AlgarveRoute3 = 354,
        BrandsHatchRoute0 = 355,
        BrandsHatchRoute1 = 356,
        BrandsHatchRoute2 = 357,
        BrandsHatchRoute3 = 358,
        IndianapolisRoute0 = 359,
        IndianapolisRoute3 = 362,
        IndianapolisRoute4 = 363,
        IndianapolisRoute6 = 365,
        IndianapolisRoute7 = 366,
        RedBullRingRoute0 = 373,
        RedBullRingRoute1 = 374,
        RedBullRingRoute2 = 375,
        RedBullRingRoute3 = 376,
        RedBullRingRoute4 = 377,
        RedBullRingRoute5 = 378,
        YasMarinaRoute0 = 379,
        YasMarinaRoute1 = 380,
        YasMarinaRoute2 = 381,
        YasMarinaRoute3 = 382,
        YasMarinaRoute4 = 383,
        SanFranciscoRoute0 = 424,
        SanFranciscoRoute1 = 425,
        SanFranciscoRoute2 = 426,
        SanFranciscoRoute3 = 427,
        SanFranciscoRoute4 = 428,
        SanFranciscoRoute5 = 429,
        WashingtonRoute0 = 431,
        WashingtonRoute1 = 432,
        WashingtonRoute2 = 433,
        WashingtonRoute3 = 434,
        WashingtonRoute4 = 435,
        WashingtonRoute5 = 436,
        MontTremblantRoute0 = 459,
        MontTremblantRoute1 = 460,
        MontTremblantRoute2 = 461,
        MontTremblantRoute3 = 462,
        MontTremblantRoute4 = 463,
        MontTremblantRoute5 = 464,
        AustinRoute0 = 471,
        AustinRoute1 = 472,
        AustinRoute2 = 473,
        AustinRoute3 = 474,
        AustinRoute4 = 475,
        AustinRoute5 = 476,
        CallahanOvalRoute0 = 477,
        CallahanOvalRoute1 = 478,
        CallahanOvalRoute2 = 479,
        CallahanOvalRoute3 = 480,
        CallahanOvalRoute4 = 481,
        CallahanOvalRoute5 = 482,
        JaramaRoute0 = 483,
        JaramaRoute1 = 484,
        HockenheimRoute0 = 485,
        HockenheimRoute1 = 486,
        HockenheimRoute2 = 487,
        HockenheimRoute3 = 488,
        HockenheimRoute4 = 489,
        HockenheimRoute5 = 490,
        OkutamaGpRoute0 = 491,
        OkutamaGpRoute1 = 492,
        OkutamaGpRoute2 = 493,
        OkutamaGpRoute3 = 494,
        OkutamaGpRoute4 = 495,
        OkutamaGpRoute5 = 496,
        SepangRoute0 = 498,
        SepangRoute1 = 499,
        SepangRoute2 = 500,
        SepangRoute3 = 501,
        SepangRoute4 = 502,
        SepangRoute5 = 503,
        IstanbulParkRoute0 = 504,
        IstanbulParkRoute1 = 505,
        IstanbulParkRoute2 = 506,
        IstanbulParkRoute3 = 507,
        YasMarinaRoute5 = 508,
        SpaRoute0 = 512,
        SpaRoute1 = 513,
        SpaRoute2 = 514,
        SpaRoute3 = 515,
        BathurstRoute0 = 516,
        BathurstRoute1 = 517,
        BrandsHatchRoute4 = 522,
        BrandsHatchRoute5 = 523,
        DetroitRoute8 = 524,
        DetroitRoute9 = 525,
        CallahanOvalRoute7 = 526,
        YasMarinaRoute7 = 527
    }

    private enum GridAutosportTrackModelConditions
    {
        ChicagoRoute3Day = 296,
        ChicagoRoute5Day = 302,
        ChicagoRoute2Day = 303,
        ParisRoute5Day = 308,
        BarcelonaRoute2Day = 310,
        BarcelonaRoute4Day = 311,
        ParisRoute3Day = 317,
        BarcelonaRoute3Day = 319,
        BarcelonaRoute5Day = 322,
        ParisRoute4Day = 323,
        RedBullRingRoute4Day = 326,
        BrandsHatchRoute2Day = 327,
        ParisRoute2Day = 333,
        DubaiRoute4Day = 336,
        DubaiRoute3Day = 340,
        YasMarinaRoute4Day = 358,
        YasMarinaRoute3Day = 359,
        YasMarinaRoute2Day = 360,
        DubaiRoute2Day = 365,
        DubaiRoute0Day = 367,
        IndianapolisRoute6Day = 386,
        RedBullRingRoute0Day = 387,
        YasMarinaRoute1Day = 388,
        BarcelonaRoute1Day = 392,
        DubaiRoute5Day = 393,
        ParisRoute1Day = 397,
        ChicagoRoute1Day = 400,
        RedBullRingRoute1Day = 408,
        IndianapolisRoute3Day = 409,
        YasMarinaRoute0Day = 410,
        BarcelonaRoute0Day = 415,
        ParisRoute0Day = 420,
        DubaiRoute1Day = 422,
        ChicagoRoute0Day = 446,
        IndianapolisRoute7Day = 477,
        YasMarinaRoute0Night = 504,
        YasMarinaRoute1Night = 505,
        YasMarinaRoute2Night = 506,
        YasMarinaRoute3Night = 507,
        YasMarinaRoute4Night = 508,
        IndianapolisRoute0Day = 509,
        ChicagoRoute4Day = 519,
        IndianapolisRoute4Day = 550,
        RedBullRingRoute2Day = 551,
        RedBullRingRoute3Day = 552,
        RedBullRingRoute5Day = 553,
        BrandsHatchRoute0Day = 554,
        BrandsHatchRoute1Day = 555,
        BrandsHatchRoute3Day = 556,
        AlgarveRoute0Day = 557,
        AlgarveRoute1Day = 558,
        AlgarveRoute2Day = 559,
        AlgarveRoute3Day = 560,
        SanFranciscoRoute0Day = 572,
        YasMarinaRoute5Day = 573,
        SanFranciscoRoute1Day = 576,
        SanFranciscoRoute2Day = 577,
        SanFranciscoRoute3Day = 578,
        SanFranciscoRoute4Day = 579,
        SanFranciscoRoute5Day = 580,
        WashingtonRoute0Day = 589,
        WashingtonRoute1Day = 590,
        WashingtonRoute2Day = 591,
        WashingtonRoute3Day = 592,
        WashingtonRoute4Day = 593,
        WashingtonRoute5Day = 594,
        MontTremblantRoute0Day = 610,
        MontTremblantRoute1Day = 611,
        MontTremblantRoute2Day = 612,
        MontTremblantRoute3Day = 613,
        MontTremblantRoute4Day = 614,
        MontTremblantRoute5Day = 615,
        Austin0Day = 622,
        Austin1Day = 623,
        Austin2Day = 624,
        Austin3Day = 625,
        Austin4Day = 626,
        Austin5Day = 627,
        CallahanOval0Day = 628,
        CallahanOval1Day = 629,
        CallahanOval2Day = 630,
        CallahanOval3Day = 631,
        CallahanOval4Day = 632,
        CallahanOval5Day = 633,
        HockenheimRoute0Day = 635,
        HockenheimRoute1Day = 636,
        HockenheimRoute2Day = 637,
        HockenheimRoute3Day = 638,
        HockenheimRoute4Day = 639,
        HockenheimRoute5Day = 640,
        JaramaRoute0Day = 642,
        JaramaRoute1Day = 643,
        OkutamaGpRoute0Day = 644,
        OkutamaGpRoute1Day = 645,
        OkutamaGpRoute2Day = 646,
        OkutamaGpRoute3Day = 647,
        OkutamaGpRoute4Day = 648,
        OkutamaGpRoute5Day = 649,
        SepangRoute0Day = 650,
        SepangRoute1Day = 651,
        SepangRoute2Day = 652,
        SepangRoute3Day = 653,
        SepangRoute4Day = 654,
        SepangRoute5Day = 655,
        IstanbulParkRoute0Day = 656,
        IstanbulParkRoute1Day = 657,
        IstanbulParkRoute2Day = 658,
        IstanbulParkRoute3Day = 659,
        BrandsHatchRoute0Night = 661,
        Austin0Night = 662,
        SepangRoute0Night = 663,
        SpaRoute0Day = 664,
        SpaRoute1Day = 665,
        SpaRoute2Day = 666,
        SpaRoute3Day = 667,
        BathurstRoute0Day = 668,
        BathurstRoute1Day = 669,
        BrandsHatchRoute4Day = 672,
        BrandsHatchRoute5Day = 673,
        DetroitRoute8Day = 674,
        DetroitRoute9Day = 676,
        CallahanOval7Day = 677,
        YasMarinaRoute7Day = 678,
        Austin1Night = 679,
        Austin2Night = 680,
        Austin3Night = 681,
        Austin4Night = 682,
        Austin5Night = 683,
        SepangRoute1Night = 684,
        SepangRoute2Night = 685,
        SepangRoute3Night = 686,
        SepangRoute4Night = 687,
        SepangRoute5Night = 688,
        BrandsHatchRoute1Night = 689,
        BrandsHatchRoute2Night = 690,
        BrandsHatchRoute3Night = 691,
        SpaRoute0Night = 692,
        SpaRoute1Night = 693,
        SpaRoute2Night = 694,
        SpaRoute3Night = 695,
        AlgarveRoute0Night = 696,
        AlgarveRoute1Night = 697,
        AlgarveRoute2Night = 698,
        AlgarveRoute3Night = 699,
        MontTremblantRoute0Night = 700,
        MontTremblantRoute1Night = 701,
        MontTremblantRoute2Night = 702,
        MontTremblantRoute3Night = 703,
        MontTremblantRoute4Night = 704,
        MontTremblantRoute5Night = 705,
        HockenheimRoute0Night = 706,
        HockenheimRoute1Night = 707,
        HockenheimRoute2Night = 708,
        HockenheimRoute3Night = 709,
        OkutamaGpRoute0Night = 712,
        OkutamaGpRoute1Night = 713,
        OkutamaGpRoute3Night = 714,
        OkutamaGpRoute4Night = 715
    }
}
