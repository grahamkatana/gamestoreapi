namespace GameStore.Api.Dtos;

public record GameDto(
    int Id,
    string Title,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);
