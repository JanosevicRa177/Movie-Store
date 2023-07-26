using FluentResults;
using MovieStoreApi.Errors;

namespace MovieStoreApi.Handlers.Http;

public static class HttpHandler
{
    public static Task<Result> Ok() => Task.FromResult(Result.Ok());
    public static Task<Result<T>> Ok<T>(T response) => Task.FromResult(Result.Ok(response));
    public static Task<Result> NotFound() => Task.FromResult(Result.Fail(new NotFoundError()));
    public static Task<Result<T>> NotFound<T>() => Task.FromResult(Result.Fail<T>(new NotFoundError()));
    public static Task<Result> BadRequest() => Task.FromResult(Result.Fail(new BadRequestError()));
    public static Task<Result<T>> BadRequest<T>() => Task.FromResult(Result.Fail<T>(new BadRequestError()));
}