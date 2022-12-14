namespace WotBlitzStatisticsPro.Application.Services
{
    public class PlayerInfoService : IPlayerInfoService
    {
        private readonly IMediator _mediator;

        public PlayerInfoService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<PlayerInfoDto> GetFullPlayerStatistics(long accountId, RequestLanguage language)
        {
            var accountStatistics = await _mediator.Send(new GetAccountStatisticsRequest(accountId, language));

            var playerInfo = accountStatistics.MapToPlayerInfoDto();

            return playerInfo;
        }
    }
}