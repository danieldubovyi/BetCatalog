# Stage 1: Base Image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Copy entrypoint.sh to handle migration and seeding
COPY ["BetCatalog.WebApi/entrypoint.sh", "/app/entrypoint.sh"]
RUN chmod +x /app/entrypoint.sh

# Stage 2: Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution file and the project files for all referenced projects
COPY ["BetCatalog.sln", "./"]
COPY ["BetCatalog.WebApi/BetCatalog.WebApi.csproj", "BetCatalog.WebApi/"]
COPY ["BetCatalog.Infrastructure.EFCore/BetCatalog.Infrastructure.EFCore.csproj", "BetCatalog.Infrastructure.EFCore/"]
COPY ["BetCatalog.Infrastructure.EFCore/Migrations/*.cs", "BetCatalog.Infrastructure.EFCore/Migrations/"]
COPY ["BetCatalog.Infrastructure.EFCore.Seed/BetCatalog.Infrastructure.EFCore.Seed.csproj", "BetCatalog.Infrastructure.EFCore.Seed/"]
COPY ["BetCatalog.Services/BetCatalog.Services.csproj", "BetCatalog.Services/"]
COPY ["BetCatalog.Models/BetCatalog.Models.csproj", "BetCatalog.Models/"]


# Restore dependencies for the WebApi project
RUN dotnet restore "BetCatalog.WebApi/BetCatalog.WebApi.csproj"

# Copy the entire solution (this includes all projects in the solution)
COPY . .

# Install EF Tools
RUN dotnet tool install --global dotnet-ef

# Add .NET SDK tools to PATH
ENV PATH="$PATH:/root/.dotnet/tools"

RUN dotnet run --project BetCatalog.Infrastructure.EFCore.Seed

# Run database migration and seed data (before publishing)
# RUN cd ./BetCatalog.Infrastructure.EFCore/
# RUN dotnet ef database update --project BetCatalog.Infrastructure.EFCore
# RUN cd ../
# RUN dotnet run --project BetCatalog.Infrastructure.EFCore.Seed

# Build the WebApi project
WORKDIR "/src/BetCatalog.WebApi"
RUN dotnet build "BetCatalog.WebApi.csproj" -c Release -o /app/build

# Stage 3: Publish Stage
FROM build AS publish
RUN dotnet publish "BetCatalog.WebApi.csproj" -c Release -o /app/publish

# Stage 4: Final Stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .


# Add entrypoint for migration and seeding
COPY ["BetCatalog.WebApi/entrypoint.sh", "/app/entrypoint.sh"]
RUN chmod +x /app/entrypoint.sh

ENTRYPOINT ["./entrypoint.sh"]
