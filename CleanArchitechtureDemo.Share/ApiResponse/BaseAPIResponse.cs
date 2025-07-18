namespace Clean_Architecture.Share.ApiResponse;

public class BaseAPIResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }
    public object? Errors { get; set; }

    private BaseAPIResponse() { }

    public static BaseAPIResponse<T> SuccessResponse(T data, string? message = null)
    {
        return new BaseAPIResponse<T>
        {
            Success = true,
            Data = data,
            Message = message,
            Errors = null
        };
    }

    public static BaseAPIResponse<T> FailResponse(string? message = null, object? errors = null)
    {
        return new BaseAPIResponse<T>
        {
            Success = false,
            Data = default,
            Message = message,
            Errors = errors
        };
    }
}
