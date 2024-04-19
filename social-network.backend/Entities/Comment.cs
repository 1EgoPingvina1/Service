namespace social_network.backend.Entities
{
    public class Comment : BaseEntity
    {
        public int PostId { get; set; }
        public Post Post { get; set; }

        public string CommentContent { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
