# Game Store API

A RESTful API built with .NET 10.0 Minimal APIs for managing a game store catalog. This API provides endpoints to manage games and genres with full CRUD operations.

## Features

- **Games Management**: Create, read, update, and delete games
- **Genre Management**: View available game genres
- **Entity Framework Core**: ORM with SQLite database
- **Database Migrations**: Automatic schema management
- **Database Seeding**: Pre-populated genre data
- **Minimal API**: Lightweight and performant endpoint routing
- **DTOs**: Clean data transfer objects for API contracts

## Tech Stack

- **.NET 10.0**: Latest .NET framework
- **ASP.NET Core Minimal APIs**: Modern, lightweight API framework
- **Entity Framework Core 10.0.1**: ORM for database operations
- **SQLite**: Lightweight, file-based database
- **C# 12**: Latest C# language features

## Project Structure

```
GameStore.Api/
├── Data/
│   ├── GameStoreContext.cs          # EF Core DbContext
│   ├── DataExtensions.cs            # Database configuration & seeding
│   └── Migrations/                  # EF Core migrations
├── Dtos/
│   ├── GameDto.cs                   # Game list view model
│   ├── GameDetailsDto.cs            # Game detail view model
│   ├── GameCreateDto.cs             # Game creation model
│   ├── GameUpdateDto.cs             # Game update model
│   └── GenreDto.cs                  # Genre view model
├── Endpoints/
│   ├── GamesEndpoints.cs            # Games API endpoints
│   └── GenresEndpoints.cs           # Genres API endpoints
├── Models/
│   ├── Game.cs                      # Game entity
│   └── Genre.cs                     # Genre entity
├── Properties/
│   └── launchSettings.json          # Launch configuration
├── Program.cs                        # Application entry point
├── appsettings.json                 # Configuration settings
├── GameStore.Api.csproj             # Project file
└── gamestore.db                     # SQLite database file
```

## Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or later
- A code editor (Visual Studio 2025, VS Code, or Rider)

## Getting Started

### 1. Clone or Extract the Project

```bash
# If cloning from a repository
git clone <repository-url>
cd GameStore.Api

# Or extract the Archive.zip file
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Run Database Migrations

The application will automatically run migrations on startup, but you can manually apply them:

```bash
dotnet ef database update
```

### 4. Run the Application

```bash
dotnet run
```

The API will start on `http://localhost:5022` by default.

### 5. Test the API

You can use the included `games.http` file with Visual Studio or REST Client extension for VS Code, or use tools like Postman or curl.

## API Endpoints

### Games

#### Get All Games
```http
GET /games
```
Returns a list of all games with their genre names.

**Response:**
```json
[
  {
    "id": 1,
    "title": "The Legend of Zelda",
    "genre": "Adventure",
    "price": 59.99,
    "releaseDate": "2024-05-12"
  }
]
```

#### Get Game by ID
```http
GET /games/{id}
```
Returns details of a specific game.

**Response:**
```json
{
  "id": 1,
  "title": "The Legend of Zelda",
  "genreId": 2,
  "price": 59.99,
  "releaseDate": "2024-05-12"
}
```

#### Create Game
```http
POST /games
Content-Type: application/json

{
  "title": "Coding Adventures",
  "genreId": 2,
  "price": 59.99,
  "releaseDate": "2024-07-01"
}
```

**Response:** `201 Created` with location header and created game details.

#### Update Game
```http
PATCH /games/{id}
Content-Type: application/json

{
  "title": "Coding Adventures Updated",
  "genreId": 3,
  "price": 49.99,
  "releaseDate": "2024-07-15"
}
```

**Response:** `200 OK`

#### Delete Game
```http
DELETE /games/{id}
```

**Response:** `204 No Content`

### Genres

#### Get All Genres
```http
GET /genres
```
Returns a list of all available game genres.

**Response:**
```json
[
  {
    "id": 1,
    "name": "Action"
  },
  {
    "id": 2,
    "name": "Adventure"
  }
]
```

## Configuration

### Database Connection

The database connection string is configured in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "GameStoreDb": "Data Source=gamestore.db"
  }
}
```

### Launch Settings

The application is configured to run on port 5022. You can modify this in `Properties/launchSettings.json`.

## Database

### Schema

#### Games Table
- **Id**: Primary key (int)
- **Title**: Game title (string, required)
- **GenreId**: Foreign key to Genres (int)
- **Price**: Game price (decimal)
- **ReleaseDate**: Release date (DateOnly)
- **CreatedAt**: Record creation timestamp (DateTime)
- **UpdatedAt**: Record update timestamp (DateTime)

#### Genres Table
- **Id**: Primary key (int)
- **Name**: Genre name (string, required)
- **CreatedAt**: Record creation timestamp (DateTime)
- **UpdatedAt**: Record update timestamp (DateTime)

### Pre-seeded Genres

The database is automatically seeded with the following genres:
- Action
- Adventure
- RPG
- Strategy
- Simulation
- Sports
- Puzzle
- Horror
- Indie

## Development

### Adding New Migrations

When you modify entity models, create a new migration:

```bash
dotnet ef migrations add <MigrationName>
```

### Updating the Database

Apply pending migrations:

```bash
dotnet ef database update
```

### Rolling Back Migrations

Revert to a previous migration:

```bash
dotnet ef database update <PreviousMigrationName>
```

### Building the Project

```bash
dotnet build
```

### Running Tests

```bash
dotnet test
```

## Notes

- The API uses SQLite for simplicity and portability
- All timestamps are stored in UTC
- The API uses minimal APIs introduced in .NET 6+ for lightweight routing
- DTOs are implemented as records for immutability and concise syntax
- Database seeding runs automatically on application startup if the Genres table is empty

## Future Enhancements

- Add authentication and authorization
- Implement pagination for game listings
- Add filtering and search capabilities
- Include game images and descriptions
- Add user reviews and ratings
- Implement caching for improved performance
- Add API versioning
- Include comprehensive unit and integration tests
- Add Swagger/OpenAPI documentation