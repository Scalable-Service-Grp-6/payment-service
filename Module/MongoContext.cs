using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PaymentService.Models;
using System.Runtime;


namespace PaymentService.Module
{

    public class MongoContext
    {
        private readonly IMongoDatabase _database;
        public MongoContext(IConfiguration configuration)
        {
            var dbSettings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>(); ;
            
            var client = new MongoClient(dbSettings?.ConnectionString);
            _database = client.GetDatabase(dbSettings?.Database);
        }

        public IMongoCollection<PaymentTransaction> Payments => _database.GetCollection<PaymentTransaction>("payments");

    }


}
