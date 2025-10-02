using MongoDB.Driver;
using Blazor_In_Insiders.Data;

namespace Blazor_In_Insiders.Services
{
    public class MongoDbService
    {
        private readonly IMongoDatabase _database;
        public MongoDbService(MongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.DatabaseName);
        }

        public virtual IMongoCollection<T> GetCollection<T>(string name) => _database.GetCollection<T>(name);
    }
}
