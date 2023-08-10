namespace MovieStoreApi.Errors;

public class BadRequestError:HttpError
{
    public BadRequestError(string message) : base(StatusCodes.Status400BadRequest, message)
    {
    }
}