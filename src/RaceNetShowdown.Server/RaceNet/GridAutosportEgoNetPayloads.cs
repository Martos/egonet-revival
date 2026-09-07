using RaceNetShowdown.Server.Data;
using RaceNetShowdown.Server.Infrastructure;
using System.Security.Cryptography;
using System.Text;
using static GridAutosportData;

namespace RaceNetShowdown.Server.RaceNet;

internal static class GridAutosportEgoNetPayloads
{
    private const string HtmlContentType = "text/html";
    private const string EgoNetContentType = "application/egonet-stream";

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
    )
    {
        public GridAutoSportChallengeRace(
            long raceNetId,
            bool higherIsBetter,
            GridAutosportDisciplineID disciplineId,
            GridAutosportTrackModelConditions conditionsId,
            GridAutosportRaceType raceTypeId,
            long raceDuration,
            int vehicleTierId,
            GridAutosportVehicleClassID vehicleClassId,
            int vehicleId,
            int vehicleDlcId,
            bool specialRace,
            long ghostSlotId,
            int rank,
            long personalBest,
            long platinumTarget,
            long goldTarget,
            long silverTarget,
            long bronzeTarget
        ) : this(
            raceNetId,
            higherIsBetter,
            disciplineId,
            conditionsId.GetTrackModel().GetLocation(),
            conditionsId.GetTrackModel(),
            0,
            conditionsId,
            raceTypeId,
            raceDuration,
            vehicleTierId,
            vehicleClassId,
            vehicleId,
            vehicleDlcId,
            specialRace,
            ghostSlotId,
            rank,
            personalBest,
            platinumTarget,
            goldTarget,
            silverTarget,
            bronzeTarget
        )
        { }
    }

    private static readonly GridAutoSportChallengeRace[] GridAutosportChallengeRaces = [
        new(1, false, GridAutosportDisciplineID.Touring, GridAutosportTrackModelConditions.OkutamaP2pRoute0Day, GridAutosportRaceType.Sprint, 0, -1, GridAutosportVehicleClassID.CatATouring, 0, 0, false, 1, -1, 0, 95776, 101373, 107599, 116333),
        new(2, false, GridAutosportDisciplineID.Endurance, GridAutosportTrackModelConditions.SanFranciscoRoute0Day, GridAutosportRaceType.Endurance, 0, -1, GridAutosportVehicleClassID.EnduranceGtGroup1, 0, 0, true, 1, -1, 98112, 95776, 101373, 107599, 116333),
        new(3, false, GridAutosportDisciplineID.Openwheel, GridAutosportTrackModelConditions.RedBullRingRoute1Day, GridAutosportRaceType.TimeAttack, 0, -1, GridAutosportVehicleClassID.FormulaA, 0, 0, false, 1, -1, 98112, 95776, 101373, 107599, 116333),
        new(4, false, GridAutosportDisciplineID.Street, GridAutosportTrackModelConditions.DubaiRoute0Day, GridAutosportRaceType.Race, 0, -1, GridAutosportVehicleClassID.HotHatch, 0, 0, false, 1, -1, 98112, 95776, 101373, 107599, 116333),
        new(5, false, GridAutosportDisciplineID.Tuner, GridAutosportTrackModelConditions.RedBullRingRoute1Day, GridAutosportRaceType.TimeAttack, 0, -1, GridAutosportVehicleClassID.Modified, 0, 0, false, 1, -1, 98112, 95776, 101373, 107599, 116333),
        new(6, false, GridAutosportDisciplineID.Endurance, GridAutosportTrackModelConditions.OkutamaP2pRoute0Day, GridAutosportRaceType.Sprint, 0, -1, GridAutosportVehicleClassID.EnduranceGtGroup2, 0, 0, false, 1, -1, 0, 95776, 101373, 107599, 116333),
    ];

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
    private static byte[] BuildChallengeRaces()
    {
        return EgoNetBinary.Dictionary(
            EgoNetBinary.Si64("RaceNetId", 798),
            EgoNetBinary.Tutc("Expires", DateTimeOffset.UtcNow.AddDays(7)),
            EgoNetBinary.Vector(
                "Races",
                GridAutosportChallengeRaces.Select(BuildChallengeRace).ToArray()));
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
    private static ulong BuildStableSteamId(string name)
    {
        var normalized = name.Trim().ToLowerInvariant();
        var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(normalized));
        var hash = BitConverter.ToUInt64(hashBytes, 0);
        return 76_561_198_000_000_000UL + hash % 10_000_000_000UL;
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
            "GRID A Player";
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
            EgoNetBinary.Vector("News", [
                EgoNetBinary.DictValue(
                    EgoNetBinary.Si64("Id", 1823425844),
                    EgoNetBinary.Tutc("ExpiresAt", DateTimeOffset.UtcNow.AddDays(7)),
                    EgoNetBinary.Si64("OwnerId", 227622574521174749),
                    EgoNetBinary.Dict("Metadata", [
                        EgoNetBinary.Dstr("header", "Notification"),
                        EgoNetBinary.DstrW("body", "Hello World"),
                        EgoNetBinary.Ui08("type", 0)
                    ])
                )
            ]));
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
            "DataMining.EndRace" => Empty(headers),
            "DataMining.PcHardware" => Empty(headers),
            "DataMining.Profile" => Empty(headers),
            "DataMining.StartEvent" => Empty(headers),
            "Statistics.SubmitRaceEndedHeatMapData" => Empty(headers),
            "RaceNetDisciplineChallenge.GetEvent" => Html(BuildChallengeRaces(), headers),
            "RaceNetDisciplineChallenge.GetRanks" => Empty(headers),
            _ => null
        };
    }
}