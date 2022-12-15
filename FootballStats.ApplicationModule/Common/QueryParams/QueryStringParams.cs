namespace FootballStats.ApplicationModule.Common.QueryParams;

public abstract class QueryStringParams
{
    const int maxPageSize = 50;
    private int _pageSize = 10;

    public int PageNumber { get; set; } = 1;
    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }

    public string? OrderBy { get; set; }

    protected QueryStringParams()
    {
    }

    protected QueryStringParams(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}