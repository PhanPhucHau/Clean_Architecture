using Clean_Architecture.Domain.Common;
using Clean_Architecture.Domain.Entities;

namespace Clean_Architecture.Domain.Events.DeviceEvent;

public class UpdateDeviceEvent : BaseEvent
{
    public Device Device { get; }

    public UpdateDeviceEvent(Device device)
    {
        Device = device;
    }
}