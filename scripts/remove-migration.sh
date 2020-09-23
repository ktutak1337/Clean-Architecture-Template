#!/bin/bash
cd src/CleanArchitectureTemplate.Infrastructure
echo "Running 'dotnet ef migrations remove --force' command..."
dotnet ef migrations remove --force