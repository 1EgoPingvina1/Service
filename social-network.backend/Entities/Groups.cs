using social_network.backend.Entities.Identity;

namespace social_network.backend.Entities
{
    public class Groups : BaseEntity
    {
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        
        public ICollection<GroupMembers> Members { get; set; }
            = new List<GroupMembers>();
    }
}
