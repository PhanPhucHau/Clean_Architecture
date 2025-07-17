using Microsoft.AspNetCore.Identity;

namespace Clean_Architecture.Infrastructure.Persistence.EntityConfigurations.Models
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public string? Description { get; set; }
    }
}
