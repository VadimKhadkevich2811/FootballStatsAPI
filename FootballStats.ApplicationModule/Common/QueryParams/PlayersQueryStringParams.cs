namespace FootballStats.ApplicationModule.Common.QueryParams;

public class PlayersQueryStringParams : QueryStringParams
{
    public string? Name { get; set; }
    public string? LastName { get; set; }

    public PlayersQueryStringParams() : base()
    {
    }
    public PlayersQueryStringParams(string? name, string? lastname) : base()
    {
        Name = name;
        LastName = lastname;
    }
    public PlayersQueryStringParams(string? name, string? lastname, string? orderBy) : this()
    {
        OrderBy = orderBy;
    }
    public PlayersQueryStringParams(int pageNumber, int pageSize) : base(pageNumber, pageSize)
    {
    }

    public PlayersQueryStringParams(int pageNumber, int pageSize, string? name, string? lastname, string? orderBy)
        : base(pageNumber, pageSize)
    {
        Name = name;
        LastName = lastname;
        OrderBy = orderBy;
    }
}