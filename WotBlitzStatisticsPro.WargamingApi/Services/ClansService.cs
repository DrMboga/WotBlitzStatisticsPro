using MediatR;
using WotBlitzStatisticsPro.Model;
using WotBlitzStatisticsPro.WargamingApi.Messages;
using WotBlitzStatisticsPro.WargamingApi.Model;

namespace WotBlitzStatisticsPro.WargamingApi.Services
{
    public class ClansService
        : IRequestHandler<GetBulkClanAccountInfosRequest, List<ClanAccountInfo>>,
        IRequestHandler<GetBulkClanInfoRequest, List<ClanInfo>>,
        IRequestHandler<GetClanAccountInfoRequest, ClanAccountInfo?>,
        IRequestHandler<GetClanInfoRequest, ClanInfo>
    {
        private readonly IWargamingClient _wargamingClient;

        public ClansService(IWargamingClient wargamingClient)
        {
            _wargamingClient = wargamingClient;
        }

        public async Task<List<ClanAccountInfo>> Handle(GetBulkClanAccountInfosRequest request, CancellationToken cancellationToken)
        {
            if (request.AccountIds.Length == 0)
            {
                return new List<ClanAccountInfo>();
            }
            var clanInfo = await _wargamingClient.GetFromBlitzApi<Dictionary<string, ClanAccountInfo>>(
                RequestLanguage.En,
                "clans/accountinfo/",
                $"account_id={string.Join(',', request.AccountIds)}&fields=account_id,clan_id,account_name").ConfigureAwait(false);
            return clanInfo?.Values.Where(c => c != null).ToList() ?? new List<ClanAccountInfo>();
        }

        public async Task<List<ClanInfo>> Handle(GetBulkClanInfoRequest request, CancellationToken cancellationToken)
        {
            if (request.ClanIds.Length == 0)
            {
                return new List<ClanInfo>();
            }
            var clanInfo = await _wargamingClient.GetFromBlitzApi<Dictionary<string, ClanInfo>>(
                request.Language,
                "clans/info/",
                $"clan_id={string.Join(',', request.ClanIds)}&fields=clan_id,tag").ConfigureAwait(false);
            return clanInfo?.Values.ToList() ?? new List<ClanInfo>();
        }

        public async Task<ClanAccountInfo?> Handle(GetClanAccountInfoRequest request, CancellationToken cancellationToken)
        {
            var clanInfo = await _wargamingClient.GetFromBlitzApi<Dictionary<string, ClanAccountInfo>>(
				request.Language,
				"clans/accountinfo/",
				$"account_id={request.AccountId}").ConfigureAwait(false);
			if (clanInfo != null && clanInfo.ContainsKey(request.AccountId.ToString()))
			{
				return clanInfo[request.AccountId.ToString()];
			}
            return null;
        }

        public async Task<ClanInfo> Handle(GetClanInfoRequest request, CancellationToken cancellationToken)
        {
			var clanInfo = await _wargamingClient.GetFromBlitzApi<Dictionary<string, ClanInfo>>(
                request.Language,
				"clans/info/",
                $"clan_id={request.ClanId}").ConfigureAwait(false);
            if (clanInfo != null && clanInfo.ContainsKey(request.ClanId.ToString()))
            {
                return clanInfo[request.ClanId.ToString()];
            }
            return new ClanInfo();
        }
    }
}