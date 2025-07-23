using Clean_Architecture.Domain.Common;

namespace Clean_Architecture.Domain.Events.FilterEvent;

public class DeleteFilterEvent : BaseEvent
{
    public int FilterId { get; set; }
    public DeleteFilterEvent(int filterId)
    {
        FilterId = filterId;
    }
}
