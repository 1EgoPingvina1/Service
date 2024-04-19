namespace social_network.backend.mongodb.settings
{
    public class MongodbSettings : IMongodbSettings
    {
        public string UserTokenCollectionName { get; set; }
        public string ConnectioonString { get; set; }
        public string DatabaseName { get; set; }
    }
}
