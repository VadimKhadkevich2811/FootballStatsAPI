using FootballStats.Application.Common.Interfaces;
using FootballStats.Application.Common.QueryParams;
using FootballStats.Application.Common.Wrappers;

namespace FootballStats.Application.Common.Helpers;

public static class PaginationHelper
{
    public static PagedResponse<IEnumerable<T>> CreatePagedReponse<T>(IEnumerable<T> pagedData, QueryStringParams validFilter,
        int totalRecords, IUriService uriService, string? route)
    {
        var response = new PagedResponse<IEnumerable<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize);
        var totalPages = (double)totalRecords / (double)validFilter.PageSize;
        int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));

        response.NextPage = validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
            ? uriService.GetPageUri(validFilter.PageNumber + 1, validFilter.PageSize, route)
            : null;
        response.PreviousPage = validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
            ? uriService.GetPageUri(validFilter.PageNumber - 1, validFilter.PageSize, route)
            : null;

        response.FirstPage = uriService.GetPageUri(1, validFilter.PageSize, route);
        response.LastPage = uriService.GetPageUri(roundedTotalPages, validFilter.PageSize, route);

        response.TotalPages = roundedTotalPages;
        response.TotalRecords = totalRecords;

        return response;
    }
}