using Clean_Architecture.Domain.Common;
using Clean_Architecture.Domain.Entities;

namespace Clean_Architecture.Domain.Events.UserEvent;

public class DeleteUserEvent : BaseEvent
{
    public DeleteUserEvent(User user)
    {
        User = user;
    }
    public User User { get; set; }
}

