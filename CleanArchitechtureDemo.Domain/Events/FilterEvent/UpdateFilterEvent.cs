using Clean_Architecture.Domain.Common;
using Clean_Architecture.Domain.Entities;

namespace Clean_Architecture.Domain.Events.FilterEvent;

public class UpdateFilterEvent : BaseEvent
{
    public UpdateFilterEvent(Filter filter)
    {
        Filter = filter;
    }
    public Filter Filter { get; set; }


}