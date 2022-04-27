# VideoGameManager

## Table of Contents

## Intro

This project is a demo-project for code-alongs and future reference, to showcase just how easy it is to build a minimal WebAPI with [ASP.NET Core][asp-webapi] in [Dotnet5][dotnet5].

[asp-webapi]: https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-5.0&tabs=visual-studio
[dotnet5]: https://docs.microsoft.com/en-us/dotnet/core/whats-new/dotnet-5

## Entity-Framework Commands

### Make migrations

```bash
# Initial Migration: use this for the first migration you make
dotnet ef migrations add Inital
```

```bash
# All other migrations: change <MigrationName> to a fitting name (being verbose helps alot)
dotnet ef migrations add <MigrationName>
```

### Apply migrations

```bash
# Use this to apply all pending migrations and to refresh the database schema
dotnet ef database update
```
