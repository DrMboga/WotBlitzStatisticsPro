namespace WotBlitzStatisticsPro.Application.Services
{
    public class PlayerRequestsHandler
        : IRequestHandler<FindPlayersRequest, List<ShortPlayerInfoDto>>,
        IRequestHandler<GetPlayerInfoRequest, PlayerInfoDto>
    {
        private readonly IFindPlayersService _findPlayersService;
        private readonly IPlayerInfoService _playerInfoService;
        private readonly IClanInfoService _clanInfoService;

        public PlayerRequestsHandler(IFindPlayersService findPlayersService, IPlayerInfoService playerInfoService, IClanInfoService clanInfoService)
        {
            _findPlayersService = findPlayersService;
            _playerInfoService = playerInfoService;
            _clanInfoService = clanInfoService;
        }

        public Task<List<ShortPlayerInfoDto>> Handle(FindPlayersRequest request, CancellationToken cancellationToken)
        {
            return _findPlayersService.FindPlayers(request.SearchString);
        }

        public async Task<PlayerInfoDto> Handle(GetPlayerInfoRequest request, CancellationToken cancellationToken)
        {
            var language = request.locale.ConvertCulture();
            var playerInfo = await _playerInfoService.GetFullPlayerStatistics(request.accountId, language);
            playerInfo.ClanInfo = await _clanInfoService.GetClanInfo(request.accountId, language);

            return playerInfo;
        }
    }
}