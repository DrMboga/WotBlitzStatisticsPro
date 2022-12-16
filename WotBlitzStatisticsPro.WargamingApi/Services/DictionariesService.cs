namespace WotBlitzStatisticsPro.WargamingApi.Services
{
    public class DictionariesService: 
        IRequestHandler<GetStaticDictionariesRequest, StaticDictionariesResponse>,
        IRequestHandler<GetDictionaryAchievements, List<WotEncyclopediaAchievementsResponse>>,
        IRequestHandler<GetDictionaryVehicles, List<WotEncyclopediaVehiclesResponse>>
    {
        private readonly IWargamingClient _wargamingClient;

        public DictionariesService(IWargamingClient wargamingClient)
        {
            _wargamingClient = wargamingClient;
        }

        public async Task<StaticDictionariesResponse> Handle(GetStaticDictionariesRequest request, CancellationToken cancellationToken)
        {
            var encyclopedia = await _wargamingClient.GetFromBlitzApi<WotEncyclopediaInfoResponse>(
                request.Language,
                "encyclopedia/info/"
            ).ConfigureAwait(false);

            var clanGlossaryResponse = await _wargamingClient.GetFromBlitzApi<WotClanMembersDictionaryResponse>(
                request.Language,
                "clans/glossary/");

            if(encyclopedia == null || clanGlossaryResponse == null)
            {
                throw new ApplicationException("Unable to get encyclopedia");
            }

            return new StaticDictionariesResponse(encyclopedia, clanGlossaryResponse);
        }

        public async Task<List<WotEncyclopediaAchievementsResponse>> Handle(GetDictionaryAchievements request, CancellationToken cancellationToken)
        {
            var response = await _wargamingClient.GetFromBlitzApi<Dictionary<string, WotEncyclopediaAchievementsResponse>>(
                request.Language,
                "encyclopedia/achievements/").ConfigureAwait(false);
            if(response == null)
            {
                return new List<WotEncyclopediaAchievementsResponse>();
            }
            return response.Values.ToList();
        }

        public async Task<List<WotEncyclopediaVehiclesResponse>> Handle(GetDictionaryVehicles request, CancellationToken cancellationToken)
        {
            var response = await _wargamingClient.GetFromBlitzApi<Dictionary<string, WotEncyclopediaVehiclesResponse>>(
                request.Language,
                "encyclopedia/vehicles/").ConfigureAwait(false);

            if(response == null)
            {
                return new List<WotEncyclopediaVehiclesResponse>();
            }
            return response.Values.ToList();
        }
    }
}