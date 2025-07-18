using Clean_Architecture.Domain.Common;
using CleanArchitechtureDemo.Domain.Enums;

namespace CleanArchitechtureDemo.Domain.Entities;

public class Filter : BaseAuditableEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public FilterType FilterType { get; set; } // e.g., "Device", "User", etc.
    public DateTime? InstallDate { get; set; }
    public DateTime? LifeDate { get; set; }
    public DateTime? LateDate { get; set; }

    public int DeviceId { get; set; }
    public virtual Device Device { get; set; }

    public virtual ICollection<Notify> Notifies { get; set; } = new HashSet<Notify>();
    public virtual ICollection<History> Histories { get; set; } = new HashSet<History>();

}
