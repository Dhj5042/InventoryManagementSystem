using FluentValidation;
using InventoryManagementSystem.Api.Database.Models;
using InventoryManagementSystem.Api.DTO;
using InventoryManagementSystem.Api.DTO.Constants;
using InventoryManagementSystem.Api.DTO.Request;
using InventoryManagementSystem.Api.Services.IServices;
using InventoryManagementSystem.API.Extension;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService,IValidator<ProductRequest> productValidator) : ControllerBase
    {


        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAll()
        {
            var products = await productService.GetAllAsync();
            return StatusCode(StatusCodes.Status200OK, new BaseResponse<List<Product>> { Result = products, Status = Constants.Success, StatusCode = StatusCodes.Status200OK, Message = Constants.Success });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(string id)
        {
            var product = await productService.GetByIdAsync(id);
            if (product == null) return NotFound();
            return StatusCode(StatusCodes.Status200OK, new BaseResponse<Product> { Result = product, Status = Constants.Success, StatusCode = StatusCodes.Status200OK, Message = Constants.Success });
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductRequest request)
        {
            var validateResult = await productValidator.ValidateAsync(request);
            if (!validateResult.IsValid)
            {
                var validationErrors = ValidationFailedResponseMapper.MapToValidationError(validateResult);
                return StatusCode(StatusCodes.Status400BadRequest, new BaseResponse<string> { StatusCode = StatusCodes.Status400BadRequest, Message = Constants.ValidationFailMessage, Errors = validationErrors });
            }
            var result = await productService.CreateAsync(request);
            return StatusCode(StatusCodes.Status201Created, new BaseResponse<CreateResponse> { Result = result, Message = string.Format(Constants.CreateSuccess, "Product") });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, ProductRequest request)
        {
            var validateResult = await productValidator.ValidateAsync(request);
            if (!validateResult.IsValid)
            {
                var validationErrors = ValidationFailedResponseMapper.MapToValidationError(validateResult);
                return StatusCode(StatusCodes.Status400BadRequest, new BaseResponse<string> { StatusCode = StatusCodes.Status400BadRequest, Message = Constants.ValidationFailMessage, Errors = validationErrors });
            }
            var existingProduct = await productService.ValidateProductId(id);
            if (existingProduct == null)
                return StatusCode(StatusCodes.Status400BadRequest, new BaseResponse<string> { Message = Constants.ValidationFailMessage, Errors = existingProduct.Errors });

            await productService.UpdateAsync(id, request);
            return StatusCode(StatusCodes.Status200OK, new BaseResponse<string> { Message = string.Format(Constants.UpdateSuccess, "Product") });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingProduct = await productService.ValidateProductId(id);
            if (existingProduct == null)
                return StatusCode(StatusCodes.Status400BadRequest, new BaseResponse<string> { Message = Constants.ValidationFailMessage, Errors = existingProduct.Errors });

            await productService.DeleteAsync(id);
            return StatusCode(StatusCodes.Status200OK, new BaseResponse<string> { Message = string.Format(Constants.DeleteSucess, "Product") });
        }

    }
}
