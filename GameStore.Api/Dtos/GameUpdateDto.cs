using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record GameUpdateDto(
    [Required][StringLength(50)][MinLength(3)] string Title,
    [Required] int GenreId,
    [Required][Range(0, 1000)] decimal Price,
    [Required] DateOnly ReleaseDate
);

