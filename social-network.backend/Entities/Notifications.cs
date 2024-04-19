using social_network.backend.Entities.Identity;

namespace social_network.backend.Entities
{
    public class Notifications : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public string NotificationDescription { get; set; }
        public DateTime Created {  get; set; } = DateTime.UtcNow;
        public bool IsReated { get; set; }
    }
}
