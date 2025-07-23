using Clean_Architecture.Domain.Common;

namespace Clean_Architecture.Domain.Events.NotifyEvent;

public class DeleteNotifyEvent : BaseEvent
{
    public int _notify;
    public DeleteNotifyEvent(int notify)
    {
        _notify = notify;
    }

}
