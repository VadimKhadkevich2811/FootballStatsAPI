namespace FootballStats.Application.Common.Wrappers;

public class PagedResponse<T> : Response<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public Uri? FirstPage { get; set; }
    public Uri? LastPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
    public Uri? NextPage { get; set; }
    public Uri? PreviousPage { get; set; }

    public PagedResponse(T data) : base(data)
    {
        PageNumber = 1;
        PageSize = 10;
    }

    public PagedResponse(T data, int pageNumber, int pageSize, string[]? errors = null, string? message = null)
        : base(data)
    {
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize > 10 ? 10 : pageSize;
    }
}