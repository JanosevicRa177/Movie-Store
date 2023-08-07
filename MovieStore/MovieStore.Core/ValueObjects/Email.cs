using System.Text.RegularExpressions;
using FluentResults;

namespace MovieStore.Core.ValueObjects;

public record Email
{
    public string Value = string.Empty;

    private Email(string value)
    {
        Value = value;
    }

    public static Result<Email> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
            return Result.Fail<Email>("Email not present");
        
        var emailRegex =new Regex("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$");
        return emailRegex.IsMatch(value) ? Result.Ok(new Email(value)) : Result.Fail<Email>(new Error("email not valid"));
    }
};