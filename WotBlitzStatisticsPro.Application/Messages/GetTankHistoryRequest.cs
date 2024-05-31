namespace WotBlitzStatisticsPro.Application;

public record GetTankHistoryRequest(long AccountId, long TankId): IRequest<SessionHistoryItemDto[]>;
