namespace WebApi.Models
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }


        public static ApiResponse<T> SuccessResponse(T data, string message = "")
        {
            return new ApiResponse<T>
            {
                IsSuccess = true,
                Data = data,
                Message = message
            };
        }

        public static ApiResponse<T> FailureResponse(string message, List<string>? errors = null)
        {
            return new ApiResponse<T>
            {
                IsSuccess = false,
                Message = message,
                Errors = errors
            };
        }
    }
}
