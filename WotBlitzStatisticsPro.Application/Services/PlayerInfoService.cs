using System.Text.Json;
using AutoMapper;
using MediatR;
using WotBlitzStatisticsPro.Application.Dto;
using WotBlitzStatisticsPro.Application.Messages;
using WotBlitzStatisticsPro.WargamingApi.Messages;
using WotBlitzStatisticsPro.WargamingApi.Model;

namespace WotBlitzStatisticsPro.Application.Services
{
    public class PlayerInfoService
        : IRequestHandler<FindPlayersRequest, List<PlayerInfoDto>>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PlayerInfoService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<List<PlayerInfoDto>> Handle(FindPlayersRequest request, CancellationToken cancellationToken)
        {
            var accountsList = await _mediator.Send(new GetAccountsListRequest(request.SearchString));
            // [{"account_id":561478103,"nickname":"GlafiGSR1989"}]

            if (accountsList == null)
            {
                return new List<PlayerInfoDto>();
            }
            var accounts = _mapper.Map<List<WotAccountListResponse>, List<PlayerInfoDto>>(accountsList);

            if (accountsList.Count > 100)
            {
                return accounts;
            }

            var accountIds = accounts.Select(a => a.AccountId).ToArray();
            var accountsBunch = await _mediator.Send(new GetBunchOfAccountsRequest(accountIds));
            // Console.WriteLine(JsonSerializer.Serialize(accountsBunch));

            // TODO: Clans and map statistics
            // TODO: Cache requests inside the client



            // Filter WOT Blitz accounts
            accounts = accounts.Where(a => a.Battles > 0).ToList();

            return accounts;

        }
    }
}