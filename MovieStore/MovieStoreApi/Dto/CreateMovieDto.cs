using FluentValidation;
using MovieStore.Core.Enum;

namespace MovieStoreApi.Dto;

public class CreateMovieDto
{
    public string Name { get; set; } = string.Empty;
    public LicensingType LicensingType { get; set; }
}
public class CreateMovieDtoValidator : AbstractValidator<CreateMovieDto> 
{
    public CreateMovieDtoValidator() 
    {
        RuleFor(x => x.Name).NotNull();
        RuleFor(x => x.LicensingType).IsInEnum();
    }
}