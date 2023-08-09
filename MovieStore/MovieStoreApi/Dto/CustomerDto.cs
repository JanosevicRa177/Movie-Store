using FluentValidation;

namespace MovieStoreApi.Dto;

public class CustomerDto
{
    public string Email { get; set; } = null!;
}
public class CustomerDtoValidator : AbstractValidator<CustomerDto> 
{
    public CustomerDtoValidator() 
    {
        RuleFor(x => x.Email).EmailAddress();
    }
}