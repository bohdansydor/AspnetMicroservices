using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public interface IMongoDbContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
