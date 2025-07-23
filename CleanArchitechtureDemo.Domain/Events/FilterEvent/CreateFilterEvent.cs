using Clean_Architecture.Domain.Common;
using Clean_Architecture.Domain.Entities;

namespace Clean_Architecture.Domain.Events.FilterEvent;

public class CreateFilterEvent : BaseEvent
{
    public Filter _filter { get; set; }
    public CreateFilterEvent(Filter filter)
    {
        _filter = filter;
    }
}
