#!/bin/bash
set -e

# Wait for the SQL Server to be ready
sleep 30

# Apply database migrations
# dotnet ef database update --project BetCatalog.Infrastructure.EFCore

# Seed the database (optional)
# dotnet run --project BetCatalog.Infrastructure.EFCore.Seed

# Start the Web API
exec dotnet BetCatalog.WebApi.dll
