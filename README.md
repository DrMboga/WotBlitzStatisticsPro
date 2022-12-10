# WotBlitzStatisticsPro

Detailed player &amp; tank statistics and history for [World of Tanks: Blitz](https://wotblitz.com/)

- Uses [Wargaming API Service](https://developers.wargaming.net/documentation/guide/getting-started/) for gathering players statistics
- Stores statistics snapshots in local database
- Allows to view players, their tanks and achievements statistics, and how it was change in time

## Dotnet CLI cheat sheet

- New solution in current folder

```bash
dotnet new sln
```

- Add existing project to solution

```bash
dotnet sln add .\WotBlitzStatisticsPro.WargamingApi.Tests\WotBlitzStatisticsPro.WargamingApi.Tests.csproj
```

- New Blazor Wasm project with PWA

```bash
dotnet new blazorwasm --pwa
```

- New Nunit project

```bash
dotnet new nunit
```

- Add nuget package

```bash
dotnet add package Automapper
```

- Add reference to existing project

```bash
dotnet add reference ..\WotBlitzStatisticsPro.Application\
```
