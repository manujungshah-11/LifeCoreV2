# ---------- Build stage ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copy csproj files first to leverage Docker layer caching on restore
COPY *.sln Directory.Build.props nuget.config ./
COPY src/LifeV2.Domain/*.csproj           src/LifeV2.Domain/
COPY src/LifeV2.Application/*.csproj       src/LifeV2.Application/
COPY src/LifeV2.Infrastructure/*.csproj    src/LifeV2.Infrastructure/
COPY src/LifeV2.API/*.csproj               src/LifeV2.API/
COPY tests/LifeV2.UnitTests/*.csproj       tests/LifeV2.UnitTests/
COPY tests/LifeV2.IntegrationTests/*.csproj tests/LifeV2.IntegrationTests/
RUN dotnet restore src/LifeV2.API/LifeV2.API.csproj

# Copy the rest and publish
COPY . .
RUN dotnet publish src/LifeV2.API/LifeV2.API.csproj -c Release -o /app/publish --no-restore

# ---------- Runtime stage ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 8080

ENTRYPOINT ["dotnet", "LifeV2.API.dll"]
