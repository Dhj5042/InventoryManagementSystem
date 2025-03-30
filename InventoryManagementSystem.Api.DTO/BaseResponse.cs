using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InventoryManagementSystem.Api.DTO
{
    public class BaseResponse<T>
    {
        public int StatusCode { get; set; }
        public string Status { get; set; }
        public T Result { get; set; }
        public string Message { get; set; }
        public List<Error> Errors { get; set; }
        public int Count { get; set; } = 0;
    }
}
