using Clean_Architecture.Domain.Common;
using Clean_Architecture.Domain.Enums;
using Clean_Architecture.Domain.Events.DeviceEvent;

namespace Clean_Architecture.Domain.Entities;

public class Device : BaseAuditableEntity
{
    public string Name { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public DateTime? InstallDate { get; set; }
    public DeviceType DeviceType { get; set; }
    public DeviceStatus DeviceStatus { get; set; }

    public int? UserId { get; set; }
    public virtual User User { get; set; }

    public virtual ICollection<Filter> DeviceUser { get; set; } = new HashSet<Filter>();

    public virtual ICollection<Notify> Notifies { get; set; } = new HashSet<Notify>();
    public virtual ICollection<History> Histories { get; set; } = new HashSet<History>();

    private Device() { }

    public static Device Create(string name, int? userId, DeviceType deviceType, DeviceStatus deviceStatus, string? brand = null, string? model = null, DateTime? installDate = null)
    {
        Validate(name, deviceType, deviceStatus);
        var device = new Device
        {
            Name = name,
            UserId = userId,
            DeviceType = deviceType,
            DeviceStatus = deviceStatus,
            Brand = brand,
            Model = model,
            InstallDate = installDate
        };

        device.AddDomainEvent(new CreateDeviceEvent(device));
        return device;
    }

    public void Update(string name, int? userId, DeviceType deviceType, DeviceStatus deviceStatus, string? brand = null, string? model = null, DateTime? installDate = null)
    {
        Validate(name, deviceType, deviceStatus);
        Name = name;
        UserId = userId;
        DeviceType = deviceType;
        DeviceStatus = deviceStatus;
        Brand = brand;
        Model = model;
        InstallDate = installDate;
        LastModified = DateTime.UtcNow;

        AddDomainEvent(new UpdateDeviceEvent(this));
    }

    public void Delete()
    {
        if (Notifies.Any() || Histories.Any())
            throw new InvalidOperationException("Cannot delete device with associated notifies or histories.");

        RemoveDomainEvent(new DeleteDeviceEvent(Id));
    }

    private static void Validate(string name, DeviceType deviceType, DeviceStatus deviceStatus)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Device name cannot be empty.");
        if (!Enum.IsDefined(typeof(DeviceType), deviceType))
            throw new ArgumentException("Invalid device type.");
        if (!Enum.IsDefined(typeof(DeviceStatus), deviceStatus))
            throw new ArgumentException("Invalid device status.");
    }

}