namespace WotBlitzStatisticsPro.Application.Services
{
    public class DictionariesHandler : 
        IRequestHandler<UpdateDictionariesRequest, DictionariesInfoDto>,
        IRequestHandler<GetLastDictionariesUpdateRequest, DictionariesInfoDto>
    {
        private readonly IMediator _mediator;
        private readonly IStaticData _staticData;

        public DictionariesHandler(IMediator mediator, IStaticData staticData)
        {
            _mediator = mediator;
            _staticData = staticData;
        }

        public async Task<DictionariesInfoDto> Handle(UpdateDictionariesRequest request, CancellationToken cancellationToken)
        {
            var language = request.locale.ConvertCulture();
            var staticDictionaries = await _mediator.Send(new GetStaticDictionariesRequest(language));
            var achievementsDictionaries = await _mediator.Send(new GetDictionaryAchievements(language));

            var vehicles = await _mediator.Send(new GetDictionaryVehicles(language));
            var vehiclesMap = await _staticData.GetTanksTreeRowMap();

            var vehicleDictionaries = vehicles.ToDbStructure(vehiclesMap);

            if(vehicleDictionaries != null)
            {
                await _mediator.Publish(new ResetVehicleDictionariesNotification(vehicleDictionaries));
                await _mediator.Publish(new UpdateStateNotification(DateTime.Now, request.locale, staticDictionaries?.EncyclopediaInfo?.GameVersion ?? "Undefined"));
            }

            return new DictionariesInfoDto(DateTime.Now, staticDictionaries?.EncyclopediaInfo?.GameVersion ?? "Undefined");
        }

        public Task<DictionariesInfoDto> Handle(GetLastDictionariesUpdateRequest request, CancellationToken cancellationToken)
        {
            // TODO: Get last date from DB (by language)
            return Task.FromResult(new DictionariesInfoDto(DateTime.Now.AddDays(-1), "0.0.1"));
        }

    }
}