using MediatR;
using WotBlitzStatisticsPro.Application.Dto;
using WotBlitzStatisticsPro.Application.Helpers;
using WotBlitzStatisticsPro.Application.Services;
using WotBlitzStatisticsPro.WebUi.Messages;

namespace WotBlitzStatisticsPro.WebUi.Services
{
    public class PlayerRequestsHandler
        : IRequestHandler<FindPlayersRequest, List<ShortPlayerInfoDto>>,
        IRequestHandler<GetPlayerInfoRequest, PlayerInfoDto>
    {
        private readonly IFindPlayersService _findPlayersService;
        private readonly IPlayerInfoService _playerInfoService;

        public PlayerRequestsHandler(IFindPlayersService findPlayersService, IPlayerInfoService playerInfoService)
        {
            _findPlayersService = findPlayersService;
            _playerInfoService = playerInfoService;
        }

        public Task<List<ShortPlayerInfoDto>> Handle(FindPlayersRequest request, CancellationToken cancellationToken)
        {
            return _findPlayersService.FindPlayers(request.SearchString);
        }

        public Task<PlayerInfoDto> Handle(GetPlayerInfoRequest request, CancellationToken cancellationToken)
        {
            return _playerInfoService.GetFullPlayerStatistics(request.accountId, request.locale.ConvertCulture());
        }
    }
}