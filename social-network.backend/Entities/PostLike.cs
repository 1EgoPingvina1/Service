using social_network.backend.Entities.Identity;

namespace social_network.backend.Entities
{
    public class PostLike : BaseEntity
    {
        public int PostId { get; set; }
        public Post Post { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
