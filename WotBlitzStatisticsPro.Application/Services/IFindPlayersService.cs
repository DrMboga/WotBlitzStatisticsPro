namespace WotBlitzStatisticsPro.Application.Services
{
    public interface IFindPlayersService
    {
        Task<List<ShortPlayerInfoDto>> FindPlayers(string searchString);
    }
}