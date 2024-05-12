using social_network.backend.Entities.Identity;

namespace social_network.backend.Entities
{
    public class Notifications : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime Created {  get; set; } = DateTime.UtcNow;
        public bool IsReated { get; set; }
    }
}
