using social_network.backend.Entities.Identity;

namespace social_network.backend.Entities
{
    public class Groups : BaseEntity
    {
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        
        //Hho create group
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
