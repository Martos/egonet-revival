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
                GridAutosportRandomizer.GenerateRandomRaces(5).Select(BuildChallengeRace).ToArray()));
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

    // ---- Profile, levels and community team ------------------------------------------------
    // The original server stored Statistics.Multiplayer from DataMining.Profile and reported the
    // online level (net_fanbase tier) as XPLevel in RaceNetTeams.GetTeamMembers.
    // Here every player with a saved profile belongs to one community team.

    private const long CommunityTeamId = 1;
    private const string CommunityTeamTag = "EGO";
    private const string CommunityTeamName = "EgoNet Revival";
    private const int DefaultTeamPageSize = 20;

    public static async Task<RaceNetResponse?> TryBuildAsync(
        string functionName,
        CapturedBody body,
        RaceNetSessionInfo? session,
        IReadOnlyDictionary<string, string> headers,
        IRaceNetStore store,
        CancellationToken cancellationToken)
    {
        switch (functionName)
        {
            case "DataMining.Profile":
            {
                var profile = EgoNetRequestParser.ReadGridAutosportProfile(body);
                if (profile is not null && session is not null)
                {
                    await store.SaveGridAutosportProfileAsync(session, profile, cancellationToken);
                }

                return Empty(headers);
            }

            case "RaceNetTeams.GetTeamDetails":
            {
                var players = await store.GetGridAutosportPlayersAsync(cancellationToken);
                return Html(BuildTeamDetails(players), headers);
            }

            case "RaceNetTeams.GetTeamMembers":
            {
                var teamId = EgoNetRequestParser.ReadTopLevelInteger(body, "TeamId");
                if (teamId is not null && teamId != CommunityTeamId)
                {
                    return Html(BuildTeamMembers([], 1, DefaultTeamPageSize), headers);
                }

                var pageNumber = (int)(EgoNetRequestParser.ReadTopLevelInteger(body, "PageNumber") ?? 1);
                var pageSize = (int)(EgoNetRequestParser.ReadTopLevelInteger(body, "PageSize") ?? DefaultTeamPageSize);
                var players = await store.GetGridAutosportPlayersAsync(cancellationToken);
                return Html(BuildTeamMembers(players, pageNumber, pageSize), headers);
            }

            default:
                return TryBuild(functionName, body, session, headers);
        }
    }

    private static byte[] BuildTeamDetails(IReadOnlyList<GridAutosportPlayerSnapshot> players)
    {
        static uint Sum(IEnumerable<int> values) => (uint)Math.Clamp(values.Sum(v => (long)Math.Max(v, 0)), 0, uint.MaxValue);

        var xp = Sum(players.Select(p => p.TotalDxp));

        return EgoNetBinary.Dictionary(
            EgoNetBinary.Dict("Details",
                EgoNetBinary.Si64("TeamId", CommunityTeamId),
                EgoNetBinary.Dstr("TeamTag", CommunityTeamTag),
                EgoNetBinary.Dstr("TeamName", CommunityTeamName),
                EgoNetBinary.Bool("HasLivery", false),
                EgoNetBinary.Blob("LiveryData", []),
                EgoNetBinary.Bool("IsOwner", false),
                EgoNetBinary.Ui32("XP", xp),
                EgoNetBinary.Ui32("XPTouring", Sum(players.Select(p => p.TouringXp))),
                EgoNetBinary.Ui32("XPEndurance", Sum(players.Select(p => p.EnduranceXp))),
                EgoNetBinary.Ui32("XPOpenWheel", Sum(players.Select(p => p.OpenWheelXp))),
                EgoNetBinary.Ui32("XPTuner", Sum(players.Select(p => p.TunerXp))),
                EgoNetBinary.Ui32("XPStreet", Sum(players.Select(p => p.StreetXp))),
                EgoNetBinary.Ui32("TotalMembers", (uint)players.Count),
                EgoNetBinary.Ui32("TeamRank", 1),
                EgoNetBinary.Ui32("TeamXPLevel", (uint)GetTeamLevel(xp))));
    }

    private static byte[] BuildTeamMembers(IReadOnlyList<GridAutosportPlayerSnapshot> players, int pageNumber, int pageSize)
    {
        pageSize = pageSize > 0 ? pageSize : DefaultTeamPageSize;
        pageNumber = Math.Max(pageNumber, 1);
        var pageCount = Math.Max(1, (players.Count + pageSize - 1) / pageSize);

        var members = players
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(player => EgoNetBinary.DictValue(
                EgoNetBinary.Dict("Presence",
                    EgoNetBinary.Ui64("SteamId", BuildStableSteamId(player.DisplayName)),
                    EgoNetBinary.Dstr("Name", player.DisplayName),
                    EgoNetBinary.Si64("EgonetId", player.PlayerProfileId)),
                EgoNetBinary.Si16("ClubPosition", 0),
                EgoNetBinary.Si32("XPLevel", player.XpLevel)))
            .ToArray();

        return EgoNetBinary.Dictionary(
            EgoNetBinary.Dict("Members",
                EgoNetBinary.Vector("MemberList", members),
                EgoNetBinary.Si32("PageCount", pageCount)));
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