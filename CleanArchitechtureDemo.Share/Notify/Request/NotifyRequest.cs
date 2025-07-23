namespace Clean_Architecture.Share.Notify.Request;

public class NotifyRequest
{
    public string Title { get; set; }
    public string Message { get; set; }
    public DateTime? NotifyDate { get; set; }
    public bool? IsRead { get; set; }
    public int UserId { get; set; }
    public int DeviceId { get; set; }
    public int FilterId { get; set; }
}
