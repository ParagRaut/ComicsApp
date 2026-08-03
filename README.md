# ComicsApp

[![.NET Core](https://github.com/ParagRaut/ComicsApp/actions/workflows/dotnetcore.yml/badge.svg)](https://github.com/ParagRaut/ComicsApp/actions/workflows/dotnetcore.yml)

With this app you can browse comics from XKCD, SMBC, Dinosaur Comics, Poorly Drawn Lines,
PHD Comics and imgflip. Pick a source, pull a random comic, and flip back and forth through
your history. <br/>

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or higher.
- A **GitHub personal access token (PAT)** with the `read:packages` scope. This project
  depends on the private [`ParagRaut.ComicsProvider`](https://github.com/ParagRaut/ComicsProvider)
  package hosted on GitHub Packages, so restoring it requires authentication.

Set the token once as a user environment variable (do this in a **new** terminal afterwards):

```powershell
setx GITHUB_TOKEN "ghp_your_token_here"
```

The `nuget.config` reads `%GITHUB_TOKEN%` to authenticate against the feed. In CI this is
provided automatically via the `PACKAGES_READ_TOKEN` secret.

## Build & run

```powershell
git clone https://github.com/ParagRaut/ComicsApp.git
cd ComicsApp
dotnet restore
dotnet build
dotnet run --project src/ComicsApp
```

Feel free to create pull requests and enjoy comic strips :)
