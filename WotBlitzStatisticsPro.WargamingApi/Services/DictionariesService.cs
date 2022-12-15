namespace WotBlitzStatisticsPro.WargamingApi.Services
{
    public class DictionariesService : IRequestHandler<GetStaticDictionariesRequest, StaticDictionariesResponse>
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
    }
}