using MediatR;
using WotBlitzStatisticsPro.Application.Dto;
using WotBlitzStatisticsPro.Application.Messages;
using WotBlitzStatisticsPro.WargamingApi.Messages;

namespace WotBlitzStatisticsPro.Application.Services
{
    public class PlayerInfoService
        : IRequestHandler<FindPlayersRequest, List<PlayerInfoDto>>
    {
        private readonly IMediator _mediator;

        public PlayerInfoService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<List<PlayerInfoDto>> Handle(FindPlayersRequest request, CancellationToken cancellationToken)
        {
            var accountsList = await _mediator.Send(new GetAccountsListRequest(request.SearchString));
            Console.WriteLine(accountsList.FirstOrDefault()?.Nickname);
            
            return new List<PlayerInfoDto> {
                new PlayerInfoDto(12345, "FakeTanker", DateTime.Now.AddMonths(-4), 33333, DateTime.Now.AddHours(-1), null, null, "YAY", 56, null),
                new PlayerInfoDto(12346, "AnotherTanker", DateTime.Now.AddMonths(-8), 333444, DateTime.Now.AddHours(-6), null, null, null, 47, null),
            };
        }
    }
}