using System.Text.Json;

namespace WotBlitzStatisticsPro.Application.Services
{
    public class DictionariesHandler : 
        IRequestHandler<UpdateDictionariesRequest, DictionariesInfoDto>,
        IRequestHandler<GetLastDictionariesUpdateRequest, DictionariesInfoDto>
    {
        private readonly IMediator _mediator;

        // private readonly IStaticData _staticData;

        public DictionariesHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<DictionariesInfoDto> Handle(UpdateDictionariesRequest request, CancellationToken cancellationToken)
        {
            var language = request.locale.ConvertCulture();
            var staticDictionaries = await _mediator.Send(new GetStaticDictionariesRequest(language));
            var achievements = await _mediator.Send(new GetDictionaryAchievements(language));
            var vehicles = await _mediator.Send(new GetDictionaryVehicles(language));
            var vehicleModules = await _mediator.Send(new GetDictionaryVehicleModules(language));

            // var vehiclesMap = await _staticData.GetTanksTreeRowMap();
            
            Console.WriteLine(JsonSerializer.Serialize(vehicleModules));
            

            // TODO: Save to DB
            return new DictionariesInfoDto(DateTime.Now, staticDictionaries?.EncyclopediaInfo?.GameVersion ?? "Undefined");
        }

        public Task<DictionariesInfoDto> Handle(GetLastDictionariesUpdateRequest request, CancellationToken cancellationToken)
        {
            // TODO: Get last date from DB (by language)
            return Task.FromResult(new DictionariesInfoDto(DateTime.Now.AddDays(-1), "0.0.1"));
        }

    }
}