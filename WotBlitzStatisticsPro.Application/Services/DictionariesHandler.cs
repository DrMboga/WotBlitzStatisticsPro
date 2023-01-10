using System.Text.Json;
using System.Text.Json.Serialization;

namespace WotBlitzStatisticsPro.Application.Services
{
    public class DictionariesHandler : 
        IRequestHandler<UpdateDictionariesRequest, DictionariesInfoDto>,
        IRequestHandler<GetLastDictionariesUpdateRequest, DictionariesInfoDto>,
        IRequestHandler<GetDictionaryVehiclesRequest, DictionaryVehicleDto[]>,
        INotificationHandler<CheckAndUpdateDictionariesNotification>,
        IRequestHandler<GetResearchedTenTierTanksCountRequest, int>
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

            // Console.WriteLine(JsonSerializer.Serialize(vehicleDictionaries, options: new()
            // {
            //     ReferenceHandler = ReferenceHandler.Preserve
            // }));

            if(vehicleDictionaries != null)
            {
                await _mediator.Publish(new ResetVehicleDictionariesNotification(vehicleDictionaries));
                await _mediator.Publish(new UpdateStateNotification(DateTime.Now, request.locale, staticDictionaries?.EncyclopediaInfo?.GameVersion ?? "Undefined"));
            }

            return await ReadState();
        }

        public Task<DictionariesInfoDto> Handle(GetLastDictionariesUpdateRequest request, CancellationToken cancellationToken)
        {
            return ReadState();
        }

        public async Task<DictionaryVehicleDto[]> Handle(GetDictionaryVehiclesRequest request, CancellationToken cancellationToken)
        {
            var vehicles = await _mediator.Send(new GetVehiclesDictionaryRequest());

            return vehicles.ToVehiclesDto();
        }

        public async Task Handle(CheckAndUpdateDictionariesNotification notification, CancellationToken cancellationToken)
        {
            var state = await ReadState();
            if(state.LastUpdateDate < DateTime.Now.AddMonths(-1))
            {
                var newState = await _mediator.Send(new UpdateDictionariesRequest(notification.language.ToString()));
            }
        }

        public Task<int> Handle(GetResearchedTenTierTanksCountRequest request, CancellationToken cancellationToken)
        {
            return _mediator.Send(new GetVehiclesCountByTierRequest(10, false));
        }

        private async Task<DictionariesInfoDto> ReadState()
        {
            var state = await _mediator.Send(new ReadStateRequest());
            return new DictionariesInfoDto(
                state.DictionariesUpdated!.Value, 
                state.GameVersion!,
                state.DictionariesLanguage,
                state.LoggedInAccountId,
                state.WgToken,
                state.WgTokenExpiration);
        }

    }
}