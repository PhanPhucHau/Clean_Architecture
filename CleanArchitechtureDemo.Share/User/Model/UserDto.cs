﻿using Clean_Architecture.Domain.Enums;

namespace Clean_Architecture.Share.User.Model;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public NotificationUser NotificationUser { get; set; }

}
