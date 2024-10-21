# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /home/app
EXPOSE 7105

# Use the SDK image for build and migration execution
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["BetCatalog.sln", "./"]
COPY ["BetCatalog.WebApi/BetCatalog.WebApi.csproj", "BetCatalog.WebApi/"]
COPY ["BetCatalog.Infrastructure.EFCore/BetCatalog.Infrastructure.EFCore.csproj", "BetCatalog.Infrastructure.EFCore/"]
COPY ["BetCatalog.Infrastructure.EFCore/Migrations/", "BetCatalog.Infrastructure.EFCore/Migrations/"]
COPY ["BetCatalog.Infrastructure.EFCore.Seed/BetCatalog.Infrastructure.EFCore.Seed.csproj", "BetCatalog.Infrastructure.EFCore.Seed/"]
COPY ["BetCatalog.Services/BetCatalog.Services.csproj", "BetCatalog.Services/"]
COPY ["BetCatalog.Models/BetCatalog.Models.csproj", "BetCatalog.Models/"]

# Restore the solution
RUN dotnet restore

# Copy full solution files for building
COPY . .

# Install EF Core tools
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Set up for migrations
WORKDIR /src

# Run migrations
ENTRYPOINT ["/bin/bash", "-c", "sleep 15 && dotnet ef database update --project BetCatalog.Infrastructure.EFCore"]
