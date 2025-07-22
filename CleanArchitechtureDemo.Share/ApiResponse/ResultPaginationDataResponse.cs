namespace Clean_Architecture.Share.ApiResponse;

public class ResultPaginationDataResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }
    public object? Errors { get; set; }
    public int TotalRecords { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    private ResultPaginationDataResponse() { }

    public static ResultPaginationDataResponse<T> SuccessResponse(T data, int totalRecords, int pageNumber, int pageSize, string? message = null)
    {
        return new ResultPaginationDataResponse<T>
        {
            Success = true,
            Data = data,
            Message = message,
            Errors = null,
            TotalRecords = totalRecords,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public static ResultPaginationDataResponse<T> FailResponse(string? message = null, object? errors = null)
    {
        return new ResultPaginationDataResponse<T>
        {
            Success = false,
            Data = default,
            Message = message,
            Errors = errors,
            TotalRecords = 0,
            PageNumber = 0,
            PageSize = 0
        };
    }
}