namespace Clean_Architecture.Share.ApiResponse;

public class PaginationRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchString { get; set; }
    public string? OrderBy { get; set; }
    public string? Order { get; set; }
}
