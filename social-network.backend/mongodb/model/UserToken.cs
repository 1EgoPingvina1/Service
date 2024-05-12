using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace social_network.backend.mongodb.model
{
    public class UserToken
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("username")]
        public string UserName { get; set; }

        [BsonElement("token")]
        public string Token { get; set; }

        [BsonElement("wasdeleted")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime WasDeleted { get; set; } = DateTime.Now;
    }
}
