using Clean_Architecture.Domain.Common;
using CleanArchitechtureDemo.Domain.Enums;

namespace CleanArchitechtureDemo.Domain.Entities;

public class Device : BaseAuditableEntity
{
    public string Name { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public DateTime? InstallDate { get; set; }
    public DeviceType DeviceType { get; set; }
    public DeviceStatus DeviceStatus { get; set; }

    public int UserId { get; set; }
    public virtual User User { get; set; }

    public virtual ICollection<Filter> DeviceUser { get; set; } = new HashSet<Filter>();

    public virtual ICollection<Notify> Notifies { get; set; } = new HashSet<Notify>();
    public virtual ICollection<History> Histories { get; set; } = new HashSet<History>();

}