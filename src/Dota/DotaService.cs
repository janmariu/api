using System.Globalization;

namespace API.Dota;

public class DotaService
{
    private readonly DotaPlayerRepository _playerRepository;
    private readonly SteamService _steamService;

    public DotaService(DotaPlayerRepository playerRepository, SteamService steamService)
    {
        _playerRepository = playerRepository;
        _steamService = steamService;
    }

    public async Task<List<DotaPlayer>> GetTeamStatus()
    {
        var dotaPlayers = _playerRepository.Get();
        var steamIds = dotaPlayers.Select(x => x.SteamId).ToArray();
        List<SteamDetails>? playerDetails = await _steamService.GetPlayerDetails(steamIds);

        if (playerDetails != null)
        {
            foreach (var player in dotaPlayers)
            {
                player.SteamDetails = playerDetails.FirstOrDefault(x => x.SteamId == player.SteamId);
            }
        }

        return dotaPlayers;
    }
}