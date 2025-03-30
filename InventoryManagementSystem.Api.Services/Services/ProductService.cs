using InventoryManagementSystem.Api.Database.Models;
using InventoryManagementSystem.Api.DTO;
using InventoryManagementSystem.Api.DTO.Constants;
using InventoryManagementSystem.Api.DTO.Request;
using InventoryManagementSystem.Api.Repository.IRepository;
using InventoryManagementSystem.Api.Services.IServices;

namespace InventoryManagementSystem.Api.Services.Services
{
    public class ProductService(IProductRepository productRepository) : IProductService
    {
        public async Task<List<Product>> GetAllAsync()
        {
            return await productRepository.GetAllAsync();
        }

        public async Task<Product?> GetByIdAsync(string id)
        {
            return await productRepository.GetByIdAsync(id);
        }
        public async Task<CreateResponse> CreateAsync(ProductRequest request)
        {
            return await productRepository.CreateAsync(request);
        }
        public async Task UpdateAsync(string id, ProductRequest request)
        {
            await productRepository.UpdateAsync(id, request);
        }

        public async Task DeleteAsync(string id)
        {
            await productRepository.DeleteAsync(id);
        }

        public async Task<ValidationResponse> ValidateProductId(string id)
        {
            ValidationResponse validationResult = new();
            if (!await productRepository.CheckGetByIdAsync(id))
            {
                validationResult.Errors.Add(new Error
                {
                    Description = string.Format(Constants.DoesNotExists, "Contact"),
                    DetailedMessage = "",
                    Field = "Id"
                });
            }
            validationResult.IsValid = validationResult.Errors?.Count == 0;
            return validationResult;
        }
    }
}
