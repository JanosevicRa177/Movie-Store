using FluentValidation;

namespace MovieStoreApi.Dto;

public class PaginationDto
{
    public int PageSize { get; set; }

    public int PageIndex { get; set; }
}
public class PaginationDtoValidator : AbstractValidator<PaginationDto> 
{
    public PaginationDtoValidator() 
    {
        RuleFor(x => x.PageSize).NotNull().GreaterThan(0);
        RuleFor(x => x.PageIndex).NotNull().GreaterThan(0);
    }
}