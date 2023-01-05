using System.Text.Json;
using System.Text.Json.Serialization;

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

            // value '{ModuleId: 1381}' is already being tracked
            Console.WriteLine(JsonSerializer.Serialize(vehicleDictionaries, options: new()
            {
                ReferenceHandler = ReferenceHandler.Preserve
            }));

            if(vehicleDictionaries != null)
            {
                // TODO: Figure out Vehicle-VehicleModule many to many relationship
                // await _mediator.Publish(new ResetVehicleDictionariesNotification(vehicleDictionaries));

                // TODO: Wait the https://github.com/JeremyLikness/SqliteWasmHelper/issues/13 issue to be closed and update the SqliteWasmHelper
                await _mediator.Publish(new UpdateStateNotification(DateTime.Now, request.locale, staticDictionaries?.EncyclopediaInfo?.GameVersion ?? "Undefined"));
            }

            return new DictionariesInfoDto(DateTime.Now, staticDictionaries?.EncyclopediaInfo?.GameVersion ?? "Undefined");
        }

        public async Task<DictionariesInfoDto> Handle(GetLastDictionariesUpdateRequest request, CancellationToken cancellationToken)
        {
            var state = await _mediator.Send(new ReadStateRequest());
            return new DictionariesInfoDto(state.DictionariesUpdated!.Value, state.GameVersion!);
        }

    }
}