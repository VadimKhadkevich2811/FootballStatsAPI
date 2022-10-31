using FootballStats.ApplicationModule.Common.Filters;

namespace FootballStats.ApplicationModule.Common.Interfaces;

public interface IUriService
{
    public Uri GetPageUri(PaginationFilter filter, string route);
}