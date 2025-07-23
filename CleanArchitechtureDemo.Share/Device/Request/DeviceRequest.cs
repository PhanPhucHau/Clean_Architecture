using Clean_Architecture.Domain.Enums;

namespace Clean_Architecture.Share.Device.Request;

public class DeviceRequest
{
    public string Name { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public DateTime? InstallDate { get; set; }
    public DeviceType DeviceType { get; set; }
    public DeviceStatus DeviceStatus { get; set; }
    public int? UserId { get; set; }
}
