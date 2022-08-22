using System.Globalization;

namespace API.Dota;

public class DotaPlayerRepository
{
    private readonly List<DotaPlayer> _players = new();
    
    public DotaPlayerRepository()
    {
        var anders = new DotaPlayer("76561198017867872", "Anders");
        anders.ExtraInfo.Add("HasTheKids", ISOWeek.GetWeekOfYear(DateTime.Now) / 2 > 0);
        _players.Add(anders);
        
        _players.Add(new DotaPlayer("76561197977798885", "Pål"));
        _players.Add(new DotaPlayer("76561198274844145", "Lee"));
        _players.Add(new DotaPlayer("76561198006174610", "Mike"));
        _players.Add(new DotaPlayer("76561198056560806", "Lars"));
        _players.Add(new DotaPlayer("76561197974437443", "Johnny"));
        _players.Add(new DotaPlayer("76561198022057846", "JMS"));
    }
    
    public List<DotaPlayer> Get()
    {
        return _players;
    }
}

public class DotaPlayer
{
    public DotaPlayer(string steamId, string name)
    {
        SteamId = steamId;
        Name = name;
    }
    public string SteamId { get; init; }
    public string Name { get; init; }

    public SteamDetails? SteamDetails { private get; set; }

    public bool PlayingDota => SteamDetails is { GameId: "570" };

    public string? NickName => SteamDetails?.PersonaName;

    public Dictionary<string, dynamic> ExtraInfo { get; set; } = new();
}