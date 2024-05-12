using social_network.backend.Entities.Identity;

namespace social_network.backend.Entities
{
    public class Post : BaseEntity
    {
        public string Title {  get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<PostLike> Likes { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
