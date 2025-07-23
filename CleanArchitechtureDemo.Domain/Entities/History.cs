using Clean_Architecture.Domain.Common;

namespace Clean_Architecture.Domain.Entities;

public class History : BaseAuditableEntity
{
    public int DeviceId { get; set; }
    public virtual Device Device { get; set; }

    public int FilterId { get; set; }
    public virtual Filter Filter { get; set; }

    public int UserId { get; set; }
    public virtual User User { get; set; }
    public int NotifyId { get; set; }
    public virtual Notify Notify { get; set; }

    public DateTime? MaintananceDate { get; set; }


}
