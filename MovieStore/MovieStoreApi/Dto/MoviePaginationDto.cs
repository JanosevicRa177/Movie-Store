using MovieStore.Core.Enum;

namespace MovieStoreApi.Dto;

public class MoviePaginationDto : PaginationDto
{
    public string? Name { get; set; } = null!;
    public LicensingType? LicensingType { get; set; }
}