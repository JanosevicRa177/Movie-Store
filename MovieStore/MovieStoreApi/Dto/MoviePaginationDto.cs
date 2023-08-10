using FluentValidation;
using MovieStore.Core.Enum;

namespace MovieStoreApi.Dto;

public class MoviePaginationDto : PaginationDto
{
    public string? Name { get; set; } = null!;
    public LicensingType? LicensingType { get; set; }
}

public class MoviePaginationDtoValidator : AbstractValidator<MoviePaginationDto> 
{
    public MoviePaginationDtoValidator() 
    {
        RuleFor(x => x.PageSize).NotNull().GreaterThanOrEqualTo(0);
        RuleFor(x => x.PageIndex).NotNull().GreaterThanOrEqualTo(0);
    }
}