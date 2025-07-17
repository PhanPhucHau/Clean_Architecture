using Microsoft.AspNetCore.Identity;
namespace Clean_Architecture.Infrastructure.Persistence.EntityConfigurations.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string? FullName { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(7);
    }
}
