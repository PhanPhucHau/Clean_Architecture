using Clean_Architecture.Domain.Common;
using Clean_Architecture.Domain.Entities;

namespace Clean_Architecture.Domain.Events.NotifyEvent;

public class CreateNotifyEvent : BaseEvent
{
    public Notify _notify { get; }
    public CreateNotifyEvent(Notify notify)
    {
        _notify = notify;
    }
}
