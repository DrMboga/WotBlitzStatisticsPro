using MediatR;

namespace WotBlitzStatisticsPro.WebUi.Messages
{
    public record IsScreenLessThenValueRequest(int WidthInPixels): IRequest<bool>;
}