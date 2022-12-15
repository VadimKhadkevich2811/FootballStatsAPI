namespace FootballStats.Application.Common.QueryParams;

public class TrainingsQueryStringParams : QueryStringParams
{
    public string? Name { get; set; }

    public TrainingsQueryStringParams() : base()
    {
    }
    public TrainingsQueryStringParams(int pageNumber, int pageSize) : base(pageNumber, pageSize)
    {
    }
    public TrainingsQueryStringParams(string? name, string? lastname, string? orderBy) : this()
    {
        OrderBy = orderBy;
    }
    public TrainingsQueryStringParams(string? name) : base()
    {
        Name = name;
    }
    public TrainingsQueryStringParams(int pageNumber, int pageSize, string? name, string? orderBy)
        : base(pageNumber, pageSize)
    {
        Name = name;
        OrderBy = orderBy;
    }
}