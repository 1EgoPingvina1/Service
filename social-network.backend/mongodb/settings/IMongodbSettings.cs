namespace social_network.backend.mongodb.settings
{
    public interface IMongodbSettings
    {
        string UserTokenCollectionName { get; set; }
        string ConnectioonString { get; set; }
        string DatabaseName { get; set; }
    }
}
