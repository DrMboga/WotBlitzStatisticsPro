namespace WotBlitzStatisticsPro.WargamingApi.Services
{
    public class PlayersService : 
        IRequestHandler<GetAccountsListRequest, List<WotAccountListResponse>>,
        IRequestHandler<GetBunchOfAccountsRequest, List<WotAccountInfo>>,
        IRequestHandler<GetAccountStatisticsRequest, WotAccountInfo>,
        IRequestHandler<GetAccountPrivateInfoRequest, WotAccountInfo>
    {
        private readonly IWargamingClient _wargamingClient;

        public PlayersService(IWargamingClient wargamingClient)
        {
            _wargamingClient = wargamingClient;
        }

        public async Task<List<WotAccountListResponse>> Handle(GetAccountsListRequest request, CancellationToken cancellationToken)
        {
            var response = await _wargamingClient.GetFromBlitzApi<List<WotAccountListResponse>>(
                RequestLanguage.En,
				"account/list/",
                $"search={request.SearchString}").ConfigureAwait(false);

            return response ?? new List<WotAccountListResponse>();
        }

        public async Task<List<WotAccountInfo>> Handle(GetBunchOfAccountsRequest request, CancellationToken cancellationToken)
        {
            var accounts = await _wargamingClient.GetFromBlitzApi<Dictionary<string, WotAccountInfo>>(
                RequestLanguage.En,
                "account/info/",
                $"account_id={string.Join(',', request.AccountIds)}&fields=account_id,nickname,last_battle_time,statistics.all.battles,statistics.all.wins").ConfigureAwait(false);

            return accounts?.Values.ToList() ?? new List<WotAccountInfo>();

        }

        public async Task<WotAccountInfo> Handle(GetAccountStatisticsRequest request, CancellationToken cancellationToken)
        {
            var account = await _wargamingClient.GetFromBlitzApi<Dictionary<string, WotAccountInfo>>(
                    request.Language,
                    "account/info/",
                    $"account_id={request.AccountId}").ConfigureAwait(false);
            return account?[request.AccountId.ToString()] ?? new WotAccountInfo();
        }

        public async Task<WotAccountInfo> Handle(GetAccountPrivateInfoRequest request, CancellationToken cancellationToken)
        {
            // https://api.wotblitz.eu/wotb/account/info/?application_id=7026c24084fc704a9b73d8b35c5ff45a&account_id=571050560&access_token=3ce71f89155c03cd3a33d0a8644396510c15ab06&fields=account_id%2Cnickname%2Cprivate
            var account = await _wargamingClient.GetFromBlitzApi<Dictionary<string, WotAccountInfo>>(
                    request.Language,
                    "account/info/",
                    $"account_id={request.AccountId}",
                    $"access_token={request.AccessToken}",
                    "fields=account_id,nickname,private").ConfigureAwait(false);
            return account?[request.AccountId.ToString()] ?? new WotAccountInfo();
        }

    }
}