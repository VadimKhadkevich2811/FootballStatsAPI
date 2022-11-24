using FootballStats.ApplicationModule.Common.Filters;

namespace FootballStats.ApplicationModule.Common.Interfaces;

public interface IUriService
{
    public Uri GetPageUri(int pageNumber, int pageSize, string? route);
}