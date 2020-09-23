#!/bin/bash
cd src/Malpa025.Infrastructure

migration_name=$1

echo "Running 'dotnet ef database update $migration_name' command..."

if [[ -n "$migration_name" ]]; then
  dotnet ef database update $migration_name
fi