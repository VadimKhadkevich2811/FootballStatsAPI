using FootballStats.Application.Common.Wrappers;

namespace FootballStats.Application.Common.Interfaces;

public interface IResponseWrapper<T, U>
    where T : class
    where U : class
{
    /// <summary>
    /// Generates the links for one specified response in the <see cref="Response{T}"/> object.
    /// </summary>
    /// <param name="response">The <see cref="Response{T}"/> instance that represents response.</param>
    /// <returns></returns>
    void GenerateLinksForOne(Response<T> response);

    /// <summary>
    /// Generates the links for many specified responses in the <see cref="Response{U}"/> object.
    /// </summary>
    /// <param name="response">The <see cref="Response{U}"/> instance that represents response.</param>
    /// <returns></returns>
    void GenerateLinksForMany(Response<U> response);
}