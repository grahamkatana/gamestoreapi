using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record GameUpdateDto(
    [Required][StringLength(50)][MinLength(3)] string Title,
    [Required][StringLength(20)] string Genre,
    [Required][Range(0, 1000)] decimal Price,
    [Required] DateOnly ReleaseDate
);

