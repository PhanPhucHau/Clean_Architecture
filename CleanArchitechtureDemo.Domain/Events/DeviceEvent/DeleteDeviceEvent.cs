using Clean_Architecture.Domain.Common;

namespace Clean_Architecture.Domain.Events.DeviceEvent;


public class DeleteDeviceEvent : BaseEvent
{
    public int DeviceId { get; }

    public DeleteDeviceEvent(int deviceId)
    {
        DeviceId = deviceId;
    }
}
