# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["Base", "Base/"]
COPY ["Weather", "Weather"]
COPY ["Weather.Console", "Weather.Console"]
RUN dotnet publish "Weather.Console/Weather.Console.csproj" -c Release -o /app/publish

# Final Stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Weather.Console.dll"]
