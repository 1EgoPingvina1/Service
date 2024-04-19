using social_network.backend.Entities.Identity;

namespace social_network.backend.Entities
{
    public class GroupMembers : BaseEntity
    {
        public int GroupId { get; set; }
        public Groups Group {  get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
