using FluentValidation;

namespace MovieStoreApi.Dto;

public class PaginationDto
{
    public int PageSize { get; set; }

    public int PageIndex { get; set; }
}