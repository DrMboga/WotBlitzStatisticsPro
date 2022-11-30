using MediatR;
using WotBlitzStatisticsPro.Application.Dto;
using WotBlitzStatisticsPro.Application.Messages;

namespace WotBlitzStatisticsPro.Application.Services
{
    public class PlayerInfoService
        : IRequestHandler<FindPlayersRequest, List<ShortPlayerInfoDto>>
    {
        private readonly IFindPlayersService _findPlayersService;

        public PlayerInfoService(IFindPlayersService findPlayersService)
        {
            _findPlayersService = findPlayersService;
        }

        public Task<List<ShortPlayerInfoDto>> Handle(FindPlayersRequest request, CancellationToken cancellationToken)
        {
            return _findPlayersService.FindPlayers(request.SearchString);
        }
    }
}