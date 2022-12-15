namespace WotBlitzStatisticsPro.Application.Services
{
    public class DictionariesHandler : IRequestHandler<UpdateDictionariesRequest, DateTime>
    {
        public async Task<DateTime> Handle(UpdateDictionariesRequest request, CancellationToken cancellationToken)
        {

            return DateTime.Now;
        }
    }
}