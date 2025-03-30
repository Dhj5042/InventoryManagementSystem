using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InventoryManagementSystem.Api.DTO
{
    public class ValidationResponse
    {
        public ValidationResponse()
        {
            Errors = new();
        }
        public bool IsValid { get; set; } = true;
        public List<Error> Errors { get; set; }
    }
}
