namespace MovieStoreApi.Errors;

public class NotFoundError: HttpError
{
    public NotFoundError() : base(StatusCodes.Status404NotFound)
    {
    }
}