using Clean_Architecture.Domain.Common;
using CleanArchitechtureDemo.Domain.Enums;

namespace CleanArchitechtureDemo.Domain.Entities;

public class User : BaseAuditableEntity
{
    public string Name { get; set; }
    public string? Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public NotificationUser NotificationUser { get; set; }
    public virtual ICollection<Device> Devices { get; set; } = new HashSet<Device>();
    public virtual ICollection<Notify> Notifies { get; set; } = new HashSet<Notify>();

}
