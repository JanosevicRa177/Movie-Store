using FluentAssertions;
using FluentResults;
using MovieStoreApi.Errors;

namespace MovieStoreApi.Test.Extensions;

public static class ResultExtension
{
    public static void ErrorShouldHave(this Result result,int statusCode)
    {
        var error = result.Errors.OfType<HttpError>().FirstOrDefault();
        error.Should().NotBe(null);
        error?.HttpStatusCode.Should().Be(statusCode);
    }
}