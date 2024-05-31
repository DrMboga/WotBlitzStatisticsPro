namespace WotBlitzStatisticsPro.Persistence.Messages;

public record GetPlayerHistoryDbRequest(long AccountId, int Take): IRequest<PlayerSession[]?>;