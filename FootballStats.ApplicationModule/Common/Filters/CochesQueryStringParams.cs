namespace FootballStats.ApplicationModule.Common.Filters;

public class CoachesQueryStringParams : QueryStringParams
{

    public CoachesQueryStringParams() : base()
    {
    }
    public CoachesQueryStringParams(int pageNumber, int pageSize) : base(pageNumber, pageSize)
    {
    }
}