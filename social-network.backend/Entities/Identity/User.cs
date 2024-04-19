using Microsoft.AspNetCore.Identity;

namespace social_network.backend.Entities.Identity
{
    public class User : IdentityUser<int>
    {
        public DateTime DateOfBirth { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        public ICollection<UserRole> UserRoles { get; set; }
        public List<Photo> Photos { get; set; } = new();

    }
}
