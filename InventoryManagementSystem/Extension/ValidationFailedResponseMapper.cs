using FluentValidation.Results;
using InventoryManagementSystem.Api.DTO;

namespace InventoryManagementSystem.API.Extension
{
    public static class ValidationFailedResponseMapper
    {
        public static List<Error> MapToValidationError(ValidationResult validateResult)
        {
            return validateResult.Errors.Select(x => new Error(
               x.ErrorMessage,
               x.CustomState == null ? x.PropertyName : x.CustomState.ToString(),
               x.PropertyName))
       .ToList();
        }
    }
}
