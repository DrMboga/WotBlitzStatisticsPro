namespace WotBlitzStatisticsPro.WargamingApi.Services
{
    public class TanksService : IRequestHandler<GetTankStatisticsRequest, WotAccountTanksStatistics[]>
    {
        private readonly IWargamingClient _wargamingClient;

        public TanksService(IWargamingClient wargamingClient)
        {
            _wargamingClient = wargamingClient;
        }

        public async Task<WotAccountTanksStatistics[]> Handle(GetTankStatisticsRequest request, CancellationToken cancellationToken)
        {
            var tanks = await _wargamingClient.GetFromBlitzApi<Dictionary<string, List<WotAccountTanksStatistics>>>(
                request.Language,
                "tanks/stats/",
                $"account_id={request.AccountId}").ConfigureAwait(false);
            return tanks?[request.AccountId.ToString()].ToArray() ?? new WotAccountTanksStatistics[0];

        }
    }
}