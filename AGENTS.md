# Repository Guidelines

## Project Structure & Module Organization

This repository is a .NET 8 facade template built on ASP.NET Core and ABP. The solution file is `FacadeCompanyName.FacadeProjectName.sln`.

- `src/FacadeCompanyName.FacadeProjectName.Application` contains application services and DTO-facing interfaces.
- `src/FacadeCompanyName.FacadeProjectName.DomainService` contains domain services, background workers, schedules, settings, localization, and navigation.
- `src/FacadeCompanyName.FacadeProjectName.DomainService.Share` contains shared constants, configuration contracts, and repository interfaces.
- `src/FacadeCompanyName.FacadeProjectName.Web.Core` contains shared web infrastructure, filters, Swagger setup, authentication, hubs, and controller base classes.
- `src/FacadeCompanyName.FacadeProjectName.Web.Host` is the runnable ASP.NET Core host with controllers, `Program.cs`, `Startup.cs`, `appsettings*.json`, and Swagger UI assets.
- `src/*SqlServer`, `src/*Oracle`, and `src/*MySql` contain provider-specific EF Core contexts, migrators, and repositories.
- `test/FacadeCompanyName.FacadeProjectName.Tests` contains automated tests.

## Build, Test, and Development Commands

- `.\build.ps1` builds the solution in `Debug` configuration.
- `.\build.ps1 -Configuration Release` builds a release configuration.
- `dotnet restore FacadeCompanyName.FacadeProjectName.sln` restores NuGet packages.
- `dotnet test FacadeCompanyName.FacadeProjectName.sln` runs the test suite.
- `dotnet run --project src\FacadeCompanyName.FacadeProjectName.Web.Host` starts the web host locally.

Before running the host, update connection strings and environment-specific settings in `src\FacadeCompanyName.FacadeProjectName.Web.Host\appsettings.Development.json`.

## Coding Style & Naming Conventions

Use C# conventions with 2-space indentation where existing project files use it and keep namespace, class, and file names aligned. Public types and members use `PascalCase`; interfaces use the `IName` pattern; private fields should follow the style already present in the edited file. Keep provider-specific code in the matching database project instead of adding conditional logic to shared modules.

## Testing Guidelines

Tests use xUnit with Shouldly and Moq. Add tests under `test/FacadeCompanyName.FacadeProjectName.Tests`, mirroring the feature area when practical, such as `Health/HealthApplication_Tests.cs`. Name test classes with a `_Tests` suffix and use clear method names describing the behavior under test. Run `dotnet test` before submitting changes.

## Commit & Pull Request Guidelines

Recent history uses short imperative or descriptive messages, for example `fix bug`, `update package version`, and `Fix config typo and improve health test`. Prefer concise messages that state the changed behavior or intent.

Pull requests should include a brief summary, testing performed, and any configuration or database-provider impact. Include screenshots only for visible Swagger UI or web-host changes, and link related issues when available.

## Security & Configuration Tips

Do not commit secrets, production connection strings, tokens, or machine-specific settings. Keep sensitive values in local configuration, environment variables, or user secrets. Review `appsettings*.json` changes carefully before committing.
