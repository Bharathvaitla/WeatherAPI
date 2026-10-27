# Weather Server

A weather REST API built with **ASP.NET Core Web API (C#)**, with JWT-based authentication.

## Features

- RESTful Web API built on ASP.NET Core
- JWT token generation and validation (`JwtHelper.cs`)
- Layered structure: controllers handle HTTP, services hold the business logic
- Environment-specific configuration via `appsettings.json` and `appsettings.Development.json`

## Tech Stack

| Area | Technology |
|---|---|
| Language | C# |
| Framework | ASP.NET Core Web API ([.NET version, e.g. .NET 8]) |
| Authentication | JWT Bearer tokens |
| Tooling | Visual Studio, `.http` file for request testing |

## Project Structure

```
Weather-server/
├── Controllers/              # API endpoints
├── Services/                 # Business logic
├── Properties/               # Launch settings
├── JwtHelper.cs              # JWT creation and validation helper
├── Program.cs                # App startup, DI and middleware configuration
├── Weather-server.csproj     # Project file
├── Weather-server.http       # Sample requests for testing
├── appsettings.json          # Base configuration
└── appsettings.Development.json
```

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) ([version])
- [API key for the weather provider, if one is used]

### Setup

```bash
git clone https://github.com/Bharathvaitla/WeatherAPI.git
cd WeatherAPI/Weather-server
dotnet restore
```

### Configuration

Add your settings in `appsettings.Development.json`, or better, use user secrets so keys are never committed:

```bash
dotnet user-secrets set "Jwt:Key" "<your-secret-key>"
dotnet user-secrets set "[Weather:ApiKey]" "<your-weather-api-key>"
```

[Adjust the key names to match your `appsettings.json`.]

### Run

```bash
dotnet run
```

The API starts on the URL shown in the console (see `Properties/launchSettings.json`).

## API Endpoints


| Method | Endpoint | Auth | Description |
|---|---|---|---|
| POST | `/api/[auth/login]` | No | Returns a JWT token |
| GET | `/api/[weather]?city={city}` | Yes | Returns weather for a city |

### Example

```http
GET /api/[weather]?city=Bengaluru
Authorization: Bearer <your-token>
```

You can also run ready-made requests from `Weather-server.http` in Visual Studio or VS Code.

## Authentication Flow

1. The client requests a token from the login endpoint.
2. The server issues a signed JWT through `JwtHelper`.
3. The client sends the token in the `Authorization: Bearer <token>` header on protected requests.

## Future Improvements

- Add xUnit unit and integration tests
- Add caching and rate limiting for weather lookups
- Containerize with Docker and deploy to Azure through CI/CD

## Author

**Bharath Vaitla** | [GitHub](https://github.com/Bharathvaitla) | bharathvaitla522@gmail.com
