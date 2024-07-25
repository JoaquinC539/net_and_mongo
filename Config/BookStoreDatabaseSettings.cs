using MongoDB.Driver;

namespace BookStoreApi.Config;

public class BookStoreDatabaseSettings
{
    public string ConnectionString { get; set; }

    public string DatabaseName { get; set; }

    
    
    public BookStoreDatabaseSettings(string ConnectionString, string DatabaseName)
    {
        this.ConnectionString = ConnectionString;
        this.DatabaseName = DatabaseName;
        
    }

    public IMongoClient GenerateMongoClient()
    {
        var mongoClient = new MongoClient(this.ConnectionString);
        return mongoClient;
    }
    public IMongoDatabase GenerateMongoDatabase()
    {
        var mongoClient=GenerateMongoClient();
        var mongoDatabase=mongoClient.GetDatabase(this.DatabaseName);
        return mongoDatabase;
    }
}