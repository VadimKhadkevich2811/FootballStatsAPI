namespace FootballStats.ApplicationModule.Common.Filters;

public class TrainingsQueryStringParams : QueryStringParams
{

    public TrainingsQueryStringParams() : base()
    {
    }
    public TrainingsQueryStringParams(int pageNumber, int pageSize) : base(pageNumber, pageSize)
    {
    }
}