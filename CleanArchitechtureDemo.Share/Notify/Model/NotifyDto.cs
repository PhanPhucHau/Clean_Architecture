using Clean_Architecture.Domain.Entities;

namespace Clean_Architecture.Share.Notify.Model;

public class NotifyDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public DateTime? NotifyDate { get; set; }
    public bool? IsRead { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }

    public int DeviceId { get; set; }

    public string DeviceName { get; set; }
    public int FilterId { get; set; }

    public string FilterName { get; set; }
}
