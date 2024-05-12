using social_network.backend.Entities.Identity;

namespace social_network.backend.Entities
{
    public class Comment : BaseEntity
    {
        public string CommentContent { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
