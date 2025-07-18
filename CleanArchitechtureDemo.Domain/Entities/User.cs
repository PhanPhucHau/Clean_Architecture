using Clean_Architecture.Domain.Common;
using Clean_Architecture.Domain.Enums;
using Clean_Architecture.Domain.Events.UserEvent;

namespace Clean_Architecture.Domain.Entities;

public class User : BaseAuditableEntity
{
    public string Name { get; set; }
    public string? Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public NotificationUser NotificationUser { get; set; }
    public virtual ICollection<Device> Devices { get; set; } = new HashSet<Device>();
    public virtual ICollection<Notify> Notifies { get; set; } = new HashSet<Notify>();


    private User() { }

    public static User Create(string name, string? email, string phoneNumber, string address, DateTime? date, Gender gender, NotificationUser notificationUser)
    {
        var user = new User
        {
            Name = name,
            Email = email,
            PhoneNumber = phoneNumber,
            Address = address,
            DateOfBirth = date,
            Gender = gender,
            NotificationUser = notificationUser
        };

        user.AddDomainEvent(new CreateUserEvent(user));

        return user;
    }

}

