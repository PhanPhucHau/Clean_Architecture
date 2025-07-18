using Clean_Architecture.Domain.Common;
using Clean_Architecture.Domain.Entities;

namespace Clean_Architecture.Domain.Events.UserEvent;

public class CreateUserEvent : BaseEvent
{
    public CreateUserEvent(User user)
    {
        User = user;
    }
    public User User { get; set; }
}
