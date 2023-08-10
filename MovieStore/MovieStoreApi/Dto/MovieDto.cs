using System.ComponentModel.DataAnnotations;
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