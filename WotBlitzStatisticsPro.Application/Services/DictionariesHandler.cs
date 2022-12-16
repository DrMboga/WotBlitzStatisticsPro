using System.Text.Json;

namespace WotBlitzStatisticsPro.Application.Services
{
    public class DictionariesHandler : 
        IRequestHandler<UpdateDictionariesRequest, DictionariesInfoDto>,
        IRequestHandler<GetLastDictionariesUpdateRequest, DictionariesInfoDto>
    {
        private readonly IMediator _mediator;

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
            
            Console.WriteLine(JsonSerializer.Serialize(vehicles));
            

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