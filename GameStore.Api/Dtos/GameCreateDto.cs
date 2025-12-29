namespace GameStore.Api.Dtos;

public record GameCreateDto(
    string Title,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);

