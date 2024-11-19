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
                return [];
            }
            try
            {
                var clanInfo = await _wargamingClient.GetFromBlitzApi<Dictionary<string, ClanAccountInfo>>(
                    RequestLanguage.En,
                    "clans/accountinfo/",
                    $"account_id={string.Join(',', request.AccountIds)}&fields=account_id,clan_id,account_name").ConfigureAwait(false);
                return clanInfo?.Values.Where(c => c != null).ToList() ?? new List<ClanAccountInfo>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return [];
            }
        }

        public async Task<List<ClanInfo>> Handle(GetBulkClanInfoRequest request, CancellationToken cancellationToken)
        {
            if (request.ClanIds.Length == 0)
            {
                return [];
            }
            try
            {
                var clanInfo = await _wargamingClient.GetFromBlitzApi<Dictionary<string, ClanInfo>>(
                    request.Language,
                    "clans/info/",
                    $"clan_id={string.Join(',', request.ClanIds)}&fields=clan_id,tag").ConfigureAwait(false);
                return clanInfo?.Values.ToList() ?? [];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return [];
            }

        }

        public async Task<ClanAccountInfo?> Handle(GetClanAccountInfoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var clanInfo = await _wargamingClient.GetFromBlitzApi<Dictionary<string, ClanAccountInfo>>(
                    request.Language,
                    "clans/accountinfo/",
                    $"account_id={request.AccountId}").ConfigureAwait(false);
                if (clanInfo != null && clanInfo.ContainsKey(request.AccountId.ToString()))
                {
                    return clanInfo[request.AccountId.ToString()];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public async Task<ClanInfo> Handle(GetClanInfoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var clanInfo = await _wargamingClient.GetFromBlitzApi<Dictionary<string, ClanInfo>>(
                    request.Language,
                    "clans/info/",
                    $"clan_id={request.ClanId}").ConfigureAwait(false);
                if (clanInfo != null && clanInfo.ContainsKey(request.ClanId.ToString()))
                {
                    return clanInfo[request.ClanId.ToString()];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new ClanInfo();
        }
    }
}