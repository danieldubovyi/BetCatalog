ARG DOTNET_RUNTIME=mcr.microsoft.com/dotnet/aspnet:8.0
ARG DOTNET_SDK=mcr.microsoft.com/dotnet/sdk:8.0

FROM ${DOTNET_RUNTIME} AS base
ENV ASPNETCORE_URLS=http://+:7105
WORKDIR /home/app
EXPOSE 7105

# Base for build
FROM ${DOTNET_SDK} AS buildbase
WORKDIR /src

COPY ["BetCatalog.sln", "./"]
COPY ["BetCatalog.WebApi/BetCatalog.WebApi.csproj", "BetCatalog.WebApi/"]
COPY ["BetCatalog.Infrastructure.EFCore/BetCatalog.Infrastructure.EFCore.csproj", "BetCatalog.Infrastructure.EFCore/"]
COPY ["BetCatalog.Infrastructure.EFCore/Migrations/*.cs", "BetCatalog.Infrastructure.EFCore/Migrations/"]
COPY ["BetCatalog.Infrastructure.EFCore.Seed/BetCatalog.Infrastructure.EFCore.Seed.csproj", "BetCatalog.Infrastructure.EFCore.Seed/"]
COPY ["BetCatalog.Services/BetCatalog.Services.csproj", "BetCatalog.Services/"]
COPY ["BetCatalog.Models/BetCatalog.Models.csproj", "BetCatalog.Models/"]

RUN dotnet restore BetCatalog.sln

COPY ["BetCatalog.Infrastructure.EFCore/", "src/"]

## Run migrations
FROM buildbase as migrations
RUN dotnet tool install --version 8.0 --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
ENTRYPOINT dotnet-ef database update --project BetCatalog.Infrastructure.EFCore
# ENTRYPOINT dotnet run --project BetCatalog.Infrastructure.EFCore.Seed