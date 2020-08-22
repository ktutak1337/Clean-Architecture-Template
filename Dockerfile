FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app
COPY . .
RUN dotnet publish src/CleanArchitectureTemplate.Api -c release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app/out .
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT ["dotnet", "CleanArchitectureTemplate.Api.dll"]