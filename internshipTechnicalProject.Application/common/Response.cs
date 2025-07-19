namespace internshipTechnicalProject.Application.Common
{
    public class Response<T>
    {
        public T? Data { get; set; } // null olabilir
        public string? Message { get; set; } // null olabilir
        public bool Success { get; set; }

        public static Response<T> SuccessResponse(T data, string message = "")
        {
            return new Response<T> { Data = data, Message = message, Success = true };
        }

        public static Response<T> FailResponse(string message)
        {
            return new Response<T> { Data = default, Message = message, Success = false };
        }
    }
}

