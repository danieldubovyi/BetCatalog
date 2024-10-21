#!/bin/bash
set -e

# Wait for SQL Server to be ready
sleep 30

# Run the database migrations
# dotnet ef database update --project BetCatalog.Infrastructure.EFCore

# Seed the database
# dotnet run --project BetCatalog.Infrastructure.EFCore.Seed

# Run the application
exec dotnet BetCatalog.WebApi.dll
