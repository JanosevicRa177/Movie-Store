namespace MovieStoreApi.Errors;

public class BadRequestError:HttpError
{
    public BadRequestError() : base(StatusCodes.Status400BadRequest)
    {
    }
}