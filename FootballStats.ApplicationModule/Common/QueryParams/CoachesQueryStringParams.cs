namespace FootballStats.ApplicationModule.Common.QueryParams;

public class CoachesQueryStringParams : QueryStringParams
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public CoachesQueryStringParams() : base()
    {
    }
    public CoachesQueryStringParams(int pageNumber, int pageSize) : base(pageNumber, pageSize)
    {
    }
    public CoachesQueryStringParams(string? name, string? lastname, string? orderBy) : this()
    {
        OrderBy = orderBy;
    }
    public CoachesQueryStringParams(string? name, string? lastname) : base()
    {
        Name = name;
        LastName = lastname;
    }
    public CoachesQueryStringParams(int pageNumber, int pageSize, string? name, string? lastname, string? orderBy) 
        : base(pageNumber, pageSize)
    {
        Name = name;
        LastName = lastname;
        OrderBy = orderBy;
    }
}