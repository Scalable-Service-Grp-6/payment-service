using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PaymentService.Models;


namespace PaymentService.Module
{

    public class MongoContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext(IConfiguration configuration)
        {
            var client = new MongoClient("mongodb://admin:admin@localhost:8000/?authSource=admin"); //configuration.GetConnectionString("MongoDb"));
            _database = client.GetDatabase("movie_booking_db");
        }

        public IMongoCollection<PaymentTransaction> Payments => _database.GetCollection<PaymentTransaction>("payments");

    }


}
