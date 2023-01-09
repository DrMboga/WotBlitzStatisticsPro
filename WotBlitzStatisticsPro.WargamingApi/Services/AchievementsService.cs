namespace WotBlitzStatisticsPro.WargamingApi.Services
{
    public class AchievementsService :
        IRequestHandler<GetAccountAchievementsRequest, WotAccountAchievementResponse>,
        IRequestHandler<GetTanksAchievementsRequest, WotAccountAchievementResponse[]>
    {
        private readonly IWargamingClient _wargamingClient;

        public AchievementsService(IWargamingClient wargamingClient)
        {
            _wargamingClient = wargamingClient;
        }

        public async Task<WotAccountAchievementResponse> Handle(GetAccountAchievementsRequest request, CancellationToken cancellationToken)
        {
            var accountAchievements = await _wargamingClient.GetFromBlitzApi<Dictionary<string, WotAccountAchievementResponse>>(
                request.Language,
                "account/achievements/",
                $"account_id={request.AccountId}").ConfigureAwait(false);
            return accountAchievements?[request.AccountId.ToString()] ?? new WotAccountAchievementResponse();        }

        public async Task<WotAccountAchievementResponse[]> Handle(GetTanksAchievementsRequest request, CancellationToken cancellationToken)
        {
            const int maxTanksCount = 100;
            var response = new List<WotAccountAchievementResponse>();

            var tankIdsQueries = new List<string>();
            if(request.TankIds == null || request.TankIds.Length == 0)
            {
                return response.ToArray();
            }

            if(request.TankIds.Length <= maxTanksCount)
            {
                tankIdsQueries.Add(string.Join(',', request.TankIds));
            }
            else
            {
                int queriesCount = request.TankIds.Length / maxTanksCount;
                // Adding one if the number is not evenly divisible
                if(request.TankIds.Length % maxTanksCount != 0)
                {
                    queriesCount++;
                }
                for (int i = 0; i < queriesCount; i++)
                {
                    var bunchOfTanks = request.TankIds
                        .Skip(i * maxTanksCount)
                        .Take(maxTanksCount)
                        .ToArray();
                    tankIdsQueries.Add(string.Join(',', bunchOfTanks));
                }
            }

            foreach (var tankIdsQuery in tankIdsQueries)
            {
                var accountAchievements = await _wargamingClient.GetFromBlitzApi<Dictionary<string, WotAccountAchievementResponse[]>>(
                    request.Language,
                    "tanks/achievements/",
                    $"account_id={request.AccountId}",
                    $"tank_id={tankIdsQuery}").ConfigureAwait(false);
                if (accountAchievements?[request.AccountId.ToString()] != null &&
                    accountAchievements?[request.AccountId.ToString()].Length > 0)
                {
                    response.AddRange(accountAchievements[request.AccountId.ToString()]);
                }
            }
            return response.ToArray();
        }
    }
}