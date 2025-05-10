using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PaymentService.DTOs;
using PaymentService.Models;
using System.Runtime;


namespace PaymentService.Module
{

    public class MongoContext
    {
        private readonly IMongoDatabase _database;
        private readonly AppSettings? _appSettings;

        public MongoContext(IConfiguration configuration, AppSettings appSettings)
        {
            _appSettings = appSettings;
            var client = new MongoClient(_appSettings?.MONGODB_URL);
            _database = client.GetDatabase(_appSettings?.MONGODB_DB_NAME);
        }

        public IMongoCollection<PaymentTransaction> Payments => _database.GetCollection<PaymentTransaction>("payments");

    }


}
