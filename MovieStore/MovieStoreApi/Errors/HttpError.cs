using FluentResults;

namespace MovieStoreApi.Errors;

public class HttpError: Error
{
    public int HttpStatusCode { get; set; }

    public HttpError(int httpStatusCode,string message)
    {
        HttpStatusCode = httpStatusCode;
        Message = message;
    }
}