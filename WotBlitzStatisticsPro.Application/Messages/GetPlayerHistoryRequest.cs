namespace WotBlitzStatisticsPro.Application;

public record class GetPlayerHistoryRequest(long AccountId): IRequest<SessionHistoryItemDto[]>;
