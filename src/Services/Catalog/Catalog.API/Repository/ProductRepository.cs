using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private IMongoDbContext _dbContext;

        public ProductRepository(IMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }   
        
        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await _dbContext.Products.Find(products => true).ToListAsync();
            return products;
        }

        public async Task<Product> GetProduct(string id)
        {
            var product = await _dbContext.Products.Find(p => p.Id == id).FirstOrDefaultAsync();

            return product;
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            var filter = Builders<Product>.Filter.ElemMatch(p => p.Name,name);

            var products = await _dbContext.Products.Find(filter).ToListAsync();

            return products;
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string category)
        {
            var filter = Builders<Product>.Filter.ElemMatch(p => p.Category, category);

            var products = await _dbContext.Products.Find(filter).ToListAsync();

            return products;
        }

        public async Task CreateProduct(Product product)
        {
            await _dbContext.Products.InsertOneAsync(product);
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updatedResult = await _dbContext.Products.ReplaceOneAsync(p => p.Id == product.Id, product);

            return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var deletedResult = await _dbContext.Products.DeleteOneAsync(filter);

            return deletedResult.IsAcknowledged && deletedResult.DeletedCount > 0;
        }


    }
}
