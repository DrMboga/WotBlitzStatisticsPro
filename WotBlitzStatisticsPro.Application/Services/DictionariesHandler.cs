namespace WotBlitzStatisticsPro.Application.Services
{
    public class DictionariesHandler : 
        IRequestHandler<UpdateDictionariesRequest, DateTime>,
        IRequestHandler<GetLastDictionariesUpdateRequest, DateTime>
    {
        public async Task<DateTime> Handle(UpdateDictionariesRequest request, CancellationToken cancellationToken)
        {

            return DateTime.Now;
        }

        public Task<DateTime> Handle(GetLastDictionariesUpdateRequest request, CancellationToken cancellationToken)
        {
            // TODO: Get last date from DB (by language)
            return Task.FromResult(DateTime.Now.AddDays(-1));
        }
    }
}