#!/bin/bash
echo "Running 'dotnet tool install --global dotnet-ef' command..."
dotnet tool install --global dotnet-ef
echo "verifying that EF Core CLI tools are correctly installed..."
echo "Running 'dotnet restore' command..."
dotnet restore
echo "Running 'dotnet ef' command..."
dotnet ef