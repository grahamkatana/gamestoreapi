namespace GameStore.Api.Dtos;

public record GameUpdateDto(
    string Title,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);

