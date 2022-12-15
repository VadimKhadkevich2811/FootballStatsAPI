using Microsoft.AspNetCore.Mvc;

namespace FootballStats.Application.Common.Wrappers;

public class Response<T>
{
    public bool Succeeded { get; set; }
    public T? Data { get; set; }
    public string? ErrorTitle { get; set; }
    public string? ErrorMessage { get; set; }

    public Response(T? data, bool succeeded = true, string? errorTitle = null, string? errorMessage = null)
    {
        Data = data;
        Succeeded = succeeded;
        ErrorTitle = errorTitle;
        ErrorMessage = errorMessage;
    }
}