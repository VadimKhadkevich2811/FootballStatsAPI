namespace FootballStats.ApplicationModule.Common.Interfaces;

public interface ISortHelper<T>
{
    /// <summary>
    /// Returns the entities sorted by the specified query string.
    /// </summary>
    /// <param name="entities">The <see cref="System.Linq.IQueryable{T}"/> instance that represents entities for sort.</param>
    /// <param name="orderByQueryString">The <see cref="System.String"/> instance that represents the order string.</param>
    /// <returns>An instance of the <see cref="System.Linq.IQueryable{T}"/> class representing the entities sorted by query string.</returns>
    IQueryable<T> ApplySort(IQueryable<T> entities, string orderByQueryString);
}