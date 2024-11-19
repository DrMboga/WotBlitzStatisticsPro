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
            try
            {
                var response = await _wargamingClient.GetFromBlitzApi<List<WotAccountListResponse>>(
                    RequestLanguage.En,
                    "account/list/",
                    $"search={request.SearchString}").ConfigureAwait(false);

                return response ?? [];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return [];
            }
        }

        public async Task<List<WotAccountInfo>> Handle(GetBunchOfAccountsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var accounts = await _wargamingClient.GetFromBlitzApi<Dictionary<string, WotAccountInfo>>(
                    RequestLanguage.En,
                    "account/info/",
                    $"account_id={string.Join(',', request.AccountIds)}&fields=account_id,nickname,last_battle_time,statistics.all.battles,statistics.all.wins").ConfigureAwait(false);

                return accounts?.Values.ToList() ?? [];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return [];
            }
        }

        public async Task<WotAccountInfo> Handle(GetAccountStatisticsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var account = await _wargamingClient.GetFromBlitzApi<Dictionary<string, WotAccountInfo>>(
                        request.Language,
                        "account/info/",
                        $"account_id={request.AccountId}").ConfigureAwait(false);
                return account?[request.AccountId.ToString()] ?? new WotAccountInfo();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new WotAccountInfo();
            }
        }

        public async Task<WotAccountInfo> Handle(GetAccountPrivateInfoRequest request, CancellationToken cancellationToken)
        {
            try 
            {
                var account = await _wargamingClient.GetFromBlitzApi<Dictionary<string, WotAccountInfo>>(
                        request.Language,
                        "account/info/",
                        $"account_id={request.AccountId}",
                        $"access_token={request.AccessToken}",
                        "fields=account_id,nickname,private").ConfigureAwait(false);
                return account?[request.AccountId.ToString()] ?? new WotAccountInfo();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new WotAccountInfo();
            }
        }

    }
}