#!/bin/bash
cd src/CleanArchitectureTemplate.Infrastructure

migration_name=$1

echo "Running 'dotnet ef migrations add $migration_name' command..."

if [[ -n "$migration_name" ]]; then
  dotnet ef migrations add $migration_name
else
  dotnet ef migrations add Initial
fi

echo "Running 'dotnet ef database update' command..."
dotnet ef database update