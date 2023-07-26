namespace MovieStoreApi.Errors;

public class ConflictError:HttpError
{
    public ConflictError() : base(StatusCodes.Status409Conflict)
    {
    }
}