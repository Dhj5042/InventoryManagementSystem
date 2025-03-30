using InventoryManagementSystem.Api.Database;
using InventoryManagementSystem.Api.Database.Models;
using InventoryManagementSystem.Api.DTO;
using InventoryManagementSystem.Api.DTO.Request;
using InventoryManagementSystem.Api.Repository.IRepository;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace InventoryManagementSystem.Api.Repository.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;

        public ProductRepository(IOptions<DBContext> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _products = database.GetCollection<Product>("Products");
        }

        public async Task<List<Product>> GetAllAsync() => await _products.Find(_ => true).ToListAsync();
        public async Task<Product?> GetByIdAsync(string id) => await _products.Find(p => p.Id == id).FirstOrDefaultAsync();
        public async Task<CreateResponse> CreateAsync(ProductRequest request)
        {
            var product = new Product
            {
                Name = request.Name,
                Category = request.Category,
                Price = request.Price,
                Quantity = request.Quantity
            };

            await _products.InsertOneAsync(product);
            return new CreateResponse { Id = product.Id }; // Assuming `Id` exists in `Product`
        }
        public async Task UpdateAsync(string id, ProductRequest request)
        {
            var product = new Product
            {
                Id = id,
                Name = request.Name,
                Category = request.Category,
                Price = request.Price,
                Quantity = request.Quantity
            };
            await _products.ReplaceOneAsync(p => p.Id == id, product);
        }
        public async Task DeleteAsync(string id) => await _products.DeleteOneAsync(p => p.Id == id);

        public async Task<bool> CheckGetByIdAsync(string id)
        {
            return await _products.Find(p => p.Id == id).AnyAsync();
        }
    }
}
