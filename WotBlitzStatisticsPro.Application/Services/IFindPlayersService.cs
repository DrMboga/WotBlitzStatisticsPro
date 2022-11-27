using WotBlitzStatisticsPro.Application.Dto;

namespace WotBlitzStatisticsPro.Application.Services
{
    public interface IFindPlayersService
    {
        Task<List<PlayerInfoDto>> FindPlayers(string searchString);
    }
}