using Clean_Architecture.Domain.Enums;

namespace Clean_Architecture.Share.Filter.Model;

public class FilterDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public FilterType FilterType { get; set; }
    public DateTime? InstallDate { get; set; }
    public DateTime? LifeDate { get; set; }
    public DateTime? LateDate { get; set; }

    public int? DeviceId { get; set; }
}
