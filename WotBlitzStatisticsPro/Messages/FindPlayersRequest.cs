namespace WotBlitzStatisticsPro.Messages
{
    public record FindPlayersRequest(string SearchString): IRequest<List<ShortPlayerInfoDto>>;
}