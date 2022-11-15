namespace FootballStats.ApplicationModule.Common.Wrappers;

public class Response<T>
{
    public bool Succeeded { get; set; }
    public T Data { get; set; }
    public string[]? Errors { get; set; }
    public string? Message { get; set; }

    public Response(T data, bool succeeded = true, string[]? errors = null, string? message = null)
    {
        Data = data;
        Succeeded = succeeded;
        Errors = errors;
        Message = message;
    }
}