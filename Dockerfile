# syntax=docker/dockerfile:1
ARG DOTNET_VERSION=10.0

# --- Build stage ---
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build

# The SDK verifies NuGet author signatures on Linux; a revoked/unreachable CRL for a
# third-party package (Refit) breaks restore in the container. Skip that check for the build.
ENV DOTNET_NUGET_SIGNATURE_VERIFICATION=false

WORKDIR /src

# Restore first (layer-cached) using only the files that affect restore.
COPY Directory.Build.props Directory.Packages.props ./
COPY src/ComicsApp.csproj src/
RUN dotnet restore src/ComicsApp.csproj

# Copy the rest and publish. Publish re-runs restore (cached above) so the
# framework static web assets under wwwroot/_framework (blazor.web.js) are emitted.
COPY . .
RUN dotnet publish src/ComicsApp.csproj -c Release -o /app/publish

# --- Runtime stage (no token baked in) ---
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS runtime
WORKDIR /app
COPY --from=build /app/publish ./

ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 10000

# Render provides $PORT at runtime; bind Kestrel to it (fallback for local runs).
CMD ["sh", "-c", "ASPNETCORE_URLS=http://+:${PORT:-10000} dotnet ComicsApp.dll"]
