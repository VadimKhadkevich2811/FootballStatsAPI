using Microsoft.AspNetCore.Mvc;

namespace FootballStats.Application.Common.Wrappers;

public class ResponseProblemDetails : ProblemDetails
{
    public ResponseProblemDetails(string? title, string? message, int statusCode, string? path)
    {
        Instance = path;
        Type = $"https://httpstatuses.com/{statusCode}";
        Status = statusCode;
        Title = title;
        Detail = message;
    }
}