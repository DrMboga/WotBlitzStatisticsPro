using MediatR;
using WotBlitzStatisticsPro.WargamingApi.Messages;
using WotBlitzStatisticsPro.WargamingApi.Model;

namespace WotBlitzStatisticsPro.WargamingApi.Services
{
    public class PlayersService : IRequestHandler<GetAccountsListRequest, List<WotAccountListResponse>>
    {
        private readonly IWargamingClient _wargamingClient;

        public PlayersService(IWargamingClient wargamingClient)
        {
            _wargamingClient = wargamingClient;
        }

        public Task<List<WotAccountListResponse>> Handle(GetAccountsListRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new List<WotAccountListResponse> {
                new WotAccountListResponse { AccountId = 1234567, Nickname="ddddf"}
            });
        }
    }
}