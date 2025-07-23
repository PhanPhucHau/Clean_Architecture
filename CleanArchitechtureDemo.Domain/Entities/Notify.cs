using Clean_Architecture.Domain.Common;
using Clean_Architecture.Domain.Events.NotifyEvent;

namespace Clean_Architecture.Domain.Entities;

public class Notify : BaseAuditableEntity
{
    public string Title { get; set; }
    public string Message { get; set; }
    public DateTime? NotifyDate { get; set; }
    public bool? IsRead { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public int DeviceId { get; set; }
    public virtual Device Device { get; set; }
    public int FilterId { get; set; }
    public virtual Filter Filter { get; set; }

    public virtual ICollection<History> Histories { get; set; } = new HashSet<History>();

    public Notify() { }

    public static Notify Create(string title, string message, DateTime? dateTime, bool? isRead, int userNotifyId, int deviceNotifyId, int filterNotifyId)
    {
        var notify = new Notify
        {
            Title = title,
            Message = message,
            NotifyDate = dateTime,
            IsRead = isRead,
            UserId = userNotifyId,
            DeviceId = deviceNotifyId,
            FilterId = filterNotifyId
        };

        notify.AddDomainEvent(new CreateNotifyEvent(notify));
        return notify;
    }

    public void Update(string title, string message, DateTime? dateTime, bool? isRead, int userNotifyId, int deviceNotifyId, int filterNotifyId)
    {
        Title = title;
        Message = message;
        NotifyDate = dateTime;
        IsRead = isRead;
        UserId = userNotifyId;
        DeviceId = deviceNotifyId;
        FilterId = filterNotifyId;

        AddDomainEvent(new UpdateNotifyEvent(this));
    }

    public void Delete()
    {
        RemoveDomainEvent(new DeleteNotifyEvent(Id));
    }
}
