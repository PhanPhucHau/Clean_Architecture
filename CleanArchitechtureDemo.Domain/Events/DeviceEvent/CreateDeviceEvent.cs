using Clean_Architecture.Domain.Common;
using Clean_Architecture.Domain.Entities;

namespace Clean_Architecture.Domain.Events.DeviceEvent;

public class CreateDeviceEvent : BaseEvent
{
    public Device Device { get; }

    public CreateDeviceEvent(Device device)
    {
        Device = device;
    }
}
