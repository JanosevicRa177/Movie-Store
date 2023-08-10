using FluentResults;
using MovieStoreApi.Errors;

namespace MovieStoreApi.Handlers;

public static class HttpHandler
{
    public static Task<Result> Ok() => Task.FromResult(Result.Ok());
    public static Task<Result<T>> Ok<T>(T response) => Task.FromResult(Result.Ok(response));
    public static Task<Result> NotFound(string message = "") => Task.FromResult(Result.Fail(new NotFoundError(message)));
    public static Task<Result<T>> NotFound<T>(string message = "") => Task.FromResult(Result.Fail<T>(new NotFoundError(message)));
    public static Task<Result> BadRequest(string message = "") => Task.FromResult(Result.Fail(new BadRequestError(message)));
    public static Task<Result<T>> BadRequest<T>(string message = "") => Task.FromResult(Result.Fail<T>(new BadRequestError(message)));
}