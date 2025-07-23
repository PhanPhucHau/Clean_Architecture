using Clean_Architecture.Domain.Common;
using Clean_Architecture.Domain.Entities;

namespace Clean_Architecture.Domain.Events.NotifyEvent;

public class UpdateNotifyEvent : BaseEvent
{
    public Notify _notify { get; set; }
    public UpdateNotifyEvent(Notify notify)
    {
        _notify = notify;
    }
}
