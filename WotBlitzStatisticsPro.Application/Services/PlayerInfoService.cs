using System.Text.Json;
using AutoMapper;
using MediatR;
using WotBlitzStatisticsPro.Application.Dto;
using WotBlitzStatisticsPro.Model;
using WotBlitzStatisticsPro.WargamingApi.Messages;
using WotBlitzStatisticsPro.WargamingApi.Model;

namespace WotBlitzStatisticsPro.Application.Services
{
    public class PlayerInfoService : IPlayerInfoService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PlayerInfoService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<PlayerInfoDto> GetFullPlayerStatistics(long accountId, RequestLanguage language)
        {
            var accountStatistics = await _mediator.Send(new GetAccountStatisticsRequest(accountId, language));

            var playerInfo = _mapper.Map<WotAccountInfo, PlayerInfoDto>(accountStatistics);
            _mapper.Map(accountStatistics.Statistics?.All, playerInfo);

            return playerInfo;
        }
    }
}