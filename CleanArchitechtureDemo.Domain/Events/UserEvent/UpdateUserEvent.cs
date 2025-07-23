using Clean_Architecture.Domain.Common;
using Clean_Architecture.Domain.Entities;

namespace Clean_Architecture.Domain.Events.UserEvent;

public class UpdateUserEvent : BaseEvent
{
    public User _user { get; }

    public UpdateUserEvent(User user)
    {
        _user = user;
    }
}