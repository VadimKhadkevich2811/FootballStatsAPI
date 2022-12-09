namespace FootballStats.ApplicationModule.Common.Interfaces;

public interface IUriService
{
    /// <summary>
    /// Returns the final route identified by the base route, page size and page number.
    /// </summary>
    /// <param name="pageNumber">The <see cref="System.Int32"/> instance that represents the page number for final Uri.</param>
    /// <param name="pageSize">The <see cref="System.Int32"/> instance that represents the page size for final Uri.</param>
    /// <param name="route">The nullable <see cref="System.String"/> instance that represents the base route for final Uri.</param>
    /// <returns>An instance of the <see cref="Uri"/> class representing the route with the specified page number, page size and base route.</returns>
    public Uri GetPageUri(int pageNumber, int pageSize, string? route);
}