using MediatR;

namespace WotBlitzStatisticsPro.WebUi.Messages
{
    public record ChangeCultureNotification(string Culture): INotification;
}