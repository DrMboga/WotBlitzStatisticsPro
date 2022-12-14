namespace WotBlitzStatisticsPro.Application.Services
{
    public class ClanInfoService : IClanInfoService
    {
        private readonly IMediator _mediator;

        public ClanInfoService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ClanInfoDto?> GetClanInfo(long accountId, RequestLanguage language)
        {
            var clanAccountInfo = await _mediator.Send(new GetClanAccountInfoRequest(accountId, language));

            if(clanAccountInfo?.ClanId == null)
            {
                return null;
            }

            var clanInfoResponse = await _mediator.Send(new GetClanInfoRequest(clanAccountInfo.ClanId.Value, language));

            var clanInfo = clanInfoResponse.MapToClanInfoDto(clanAccountInfo);

            // TODO: Send mediatR request to get RoleLocalized by Role

            return clanInfo;
        }
    }
}