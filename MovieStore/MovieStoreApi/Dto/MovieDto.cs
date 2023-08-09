using System.ComponentModel.DataAnnotations;
using FluentValidation;
using MovieStore.Core.Enum;

namespace MovieStoreApi.Dto;

public class MovieDto
{
    [Required]
    public Guid Id { get; private set; }
    [Required]
    public string Name { get; private set; } = null!;
    [Required]
    public LicensingType LicensingType { get; protected set; }
}
public class MovieDtoValidator : AbstractValidator<MovieDto> 
{
    public MovieDtoValidator() 
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Name).NotNull();
        RuleFor(x => x.LicensingType).IsInEnum();
    }
}