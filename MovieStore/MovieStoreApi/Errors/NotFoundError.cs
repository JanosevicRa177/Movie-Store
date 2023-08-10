namespace MovieStoreApi.Errors;

public class NotFoundError: HttpError
{
    public NotFoundError(string message) : base(StatusCodes.Status404NotFound, message)
    {
    }
}