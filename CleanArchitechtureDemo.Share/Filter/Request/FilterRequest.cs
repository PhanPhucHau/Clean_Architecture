using Clean_Architecture.Domain.Enums;

namespace Clean_Architecture.Share.Filter.Request;

public class FilterRequest
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public FilterType FilterType { get; set; }
    public DateTime? InstallDate { get; set; }
    public DateTime? LifeDate { get; set; }
    public DateTime? LateDate { get; set; }

    public int? DeviceId { get; set; }
}
