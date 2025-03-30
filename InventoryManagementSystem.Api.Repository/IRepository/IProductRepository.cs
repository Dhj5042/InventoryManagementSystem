using InventoryManagementSystem.Api.Database.Models;
using InventoryManagementSystem.Api.DTO;
using InventoryManagementSystem.Api.DTO.Request;

namespace InventoryManagementSystem.Api.Repository.IRepository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(string id);
        Task<CreateResponse> CreateAsync(ProductRequest request);
        Task UpdateAsync(string id, ProductRequest request);
        Task DeleteAsync(string id);

        Task<bool> CheckGetByIdAsync(string id);

    }
}
