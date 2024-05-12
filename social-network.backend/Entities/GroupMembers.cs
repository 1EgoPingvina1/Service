using social_network.backend.Entities.Identity;

namespace social_network.backend.Entities
{
    public class GroupMembers : BaseEntity
    {
        public string Username { get; set; }

        public int GroupId { get; set; }
        public Groups Group {  get; set; }
    }
}
