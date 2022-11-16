using MediatR;
using WotBlitzStatisticsPro.WebUi.Model;

namespace WotBlitzStatisticsPro.WebUi.Messages
{
    /// <summary>
    /// Request for read data from local storage
    /// </summary>
    public class LocalStateRequest: IRequest<LocalState> { }
}