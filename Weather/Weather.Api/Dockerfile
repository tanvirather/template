# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["Base", "Base/"]
COPY ["Weather/Weather", "Weather/Weather"]
COPY ["Weather/Weather.Api", "Weather/Weather.Api"]
RUN dotnet publish "Weather/Weather.Api/Weather.Api.csproj" -c Release -o /app/publish

# Final Stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Weather.Api.dll"]
