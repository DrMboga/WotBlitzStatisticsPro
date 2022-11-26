using MediatR;
using WotBlitzStatisticsPro.Application.Dto;
using WotBlitzStatisticsPro.Application.Messages;

namespace WotBlitzStatisticsPro.Application.Services
{
    public class PlayerInfoService
        : IRequestHandler<FindPlayersRequest, List<PlayerInfoDto>>
    {
        public Task<List<PlayerInfoDto>> Handle(FindPlayersRequest request, CancellationToken cancellationToken)
        {
            // TODO: Request from WG API
            return Task.FromResult(new List<PlayerInfoDto> {
                new PlayerInfoDto(12345, "FakeTanker", DateTime.Now.AddMonths(-4), 33333, DateTime.Now.AddHours(-1), null, null, "YAY", 56, null),
                new PlayerInfoDto(12346, "AnotherTanker", DateTime.Now.AddMonths(-8), 333444, DateTime.Now.AddHours(-6), null, null, null, 47, null),
            });
        }
    }
}