namespace WotBlitzStatisticsPro.Application.Services
{
    public class FindPlayersService : IFindPlayersService
    {
        private readonly IMediator _mediator;

        public FindPlayersService(IMediator mediator)
        {
            _mediator = mediator;
        }


        public async Task<List<ShortPlayerInfoDto>> FindPlayers(string searchString)
        {
            var accountsList = await _mediator.Send(new GetAccountsListRequest(searchString));

            if (accountsList == null)
            {
                return new List<ShortPlayerInfoDto>();
            }
            var accounts = accountsList.Select(a => a.MapToShortPlayerInfoDto()).ToList();

            if (accountsList.Count > 100)
            {
                return accounts;
            }

            var accountIds = accounts.Select(a => a.AccountId).ToArray();
            var accountsBunch = await _mediator.Send(new GetBunchOfAccountsRequest(accountIds));

            var clanAccounts = await _mediator.Send(new GetBulkClanAccountInfosRequest(accountIds));

            List<ClanInfo>? clans = null;
            if (clanAccounts != null)
            {
                clans = await _mediator.Send(new GetBulkClanInfoRequest(clanAccounts.Where(c => c.ClanId.HasValue).Select(c => c.ClanId!.Value).ToArray()));
            }

            for (int i = 0; i < accounts.Count; i++)
            {
                var accountResponse = accounts.ToArray()[i];
                var shortAccountInfo = accountsBunch?.FirstOrDefault(a => a != null && a.AccountId == accountResponse.AccountId);
                if (shortAccountInfo != null)
                {
                    accountResponse.MapToShortPlayerInfoDto(shortAccountInfo);
                }

                // Clan
                if (clanAccounts != null && clans != null)
                {
                    var aClan = clanAccounts.FirstOrDefault(c => c.AccountId == accountResponse.AccountId);
                    var clan = clans.FirstOrDefault(c => c.ClanId == aClan?.ClanId);
                    if (clan != null)
                    {
                        accountResponse.ClanTag = clan.Tag;
                    }
                }
            }

            // Filter WOT Blitz accounts
            accounts = accounts.Where(a => a.Battles > 0).ToList();

            return accounts;
        }
    }
}