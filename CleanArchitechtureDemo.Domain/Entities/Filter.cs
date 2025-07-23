using Clean_Architecture.Domain.Common;
using Clean_Architecture.Domain.Enums;
using Clean_Architecture.Domain.Events.FilterEvent;
using Clean_Architecture.Domain.Events.NotifyEvent;
using System.Threading;

namespace Clean_Architecture.Domain.Entities;

public class Filter : BaseAuditableEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public FilterType FilterType { get; set; } 
    public DateTime? InstallDate { get; set; }
    public DateTime? LifeDate { get; set; }
    public DateTime? LateDate { get; set; }

    public int? DeviceId { get; set; }
    public virtual Device Device { get; set; }

    public virtual ICollection<Notify> Notifies { get; set; } = new HashSet<Notify>();
    public virtual ICollection<History> Histories { get; set; } = new HashSet<History>();

    public Filter() { }

    public static Filter Create(string name, string? description, FilterType filterType, DateTime? installDate, DateTime? lifeDate, DateTime? lateDate, int? deviceId)
    {
        var filter = new Filter
        {
            Name = name,
            Description = description,
            FilterType = filterType,
            InstallDate = installDate,
            LifeDate = lifeDate,
            LateDate = lateDate,
            DeviceId = deviceId
        };

        filter.AddDomainEvent(new CreateFilterEvent(filter));
        return filter;
    }

    public void Update(string name, string? description, FilterType filterType, DateTime? installDate, DateTime? lifeDate, DateTime? lateDate, int? deviceId)
    {
        Name = name;
        Description = description;
        FilterType = filterType;
        InstallDate = installDate;
        LifeDate = lifeDate;
        LateDate = lateDate;
        DeviceId = deviceId;

        AddDomainEvent(new UpdateFilterEvent(this));
    }

    public void Delete()
    {
        RemoveDomainEvent(new DeleteFilterEvent(Id));
    }

}
