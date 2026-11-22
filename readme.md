# NLayer and Clean Architecture Best Practices

Two independent ASP.NET Core samples target **.NET 10 (C# 14)**:

- **Clean Architecture:** `API` → `Application` → `Domain`, with `Persistence`, `Caching`, and `Bus` supporting the API.
- **N-Layer Architecture:** `App.API` → `App.Services` → `App.Repositories`.

## Getting started

Install the .NET 10 SDK. `global.json` allows compatible .NET 10 SDK feature bands.

```bash
dotnet restore NLayerAndCleanArchitectureBestPractices.sln
dotnet build NLayerAndCleanArchitectureBestPractices.sln --configuration Release
dotnet test NLayerAndCleanArchitectureBestPractices.sln --configuration Release
dotnet run --project API/API.csproj
# or
dotnet run --project App.API/App.API.csproj
```

Configure SQL Server connections for each API; the Clean Architecture sample also uses RabbitMQ.

## Service result pattern

Both architectures expose `ServiceResult<T>` and `ServiceResult`. The factories `Success`, `SuccessAsCreated`, and `Fail` return either a nested sealed `Succeeded` or `Failed` record. Only a success contains typed data; only a failure contains errors.

Each application's `CustomBaseController` translates the result to an HTTP response through pattern matching. Existing HTTP payloads with `data` and `errorMessages` properties are retained for compatibility.

## Migration notes

- All nine production projects and the tests target `net10.0`.
- EF Core SQL Server, Design and Tools were aligned to `10.0.14`; existing EF migrations were preserved.
- Removed the mismatched `MassTransit.AspNetCore` 7.x package; `MassTransit.RabbitMQ` 8.x already supplies the `AddMassTransit` integration.
- AutoMapper and Swashbuckle are kept on their existing major versions to avoid unrelated breaking API upgrades.
- `FluentValidation.AspNetCore` has been patched to `11.3.1`, but is a legacy integration. Plan its removal in a separate change and move to explicit validation.
- Review EF Core 10 breaking changes and generate/review migrations against your own SQL Server before deploying. No database migrations were generated as part of the SDK upgrade.

GitHub Actions builds and runs regression tests on .NET 10.
