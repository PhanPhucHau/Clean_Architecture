using Clean_Architecture.Domain.Common;

namespace Clean_Architecture.Domain.Entities;

public class Notify : BaseAuditableEntity
{
    public string Title { get; set; }
    public string Message { get; set; }
    public DateTime? NotifyDate { get; set; }
    public bool? IsRead { get; set; }
    public string UserNotifyId { get; set; }
    public virtual User User { get; set; }
    public string DeviceNotifyId { get; set; }
    public virtual Device Device { get; set; }
    public string FilterNotifyId { get; set; }
    public virtual Filter Filter { get; set; }

    public virtual ICollection<History> Histories { get; set; } = new HashSet<History>();
}
