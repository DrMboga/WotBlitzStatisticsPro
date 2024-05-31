namespace WotBlitzStatisticsPro.Persistence.Messages;

public record GetTankHistoryDbRequest(long AccountId, long TankId, int Take): IRequest<PlayerTankSession[]?>;
