using DataAccessLayer.Models;
using DataAccsesLayer.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace DataAccsesLayer;

public class AppDbContext 
{
    public IMongoDatabase _database { get; }

    public AppDbContext(string connecton, string database)
    {
        var client = new MongoClient(connecton);
        _database = client.GetDatabase(database);
    }

    public IMongoCollection<User> Users
     => _database.GetCollection<User>("Users");
}
