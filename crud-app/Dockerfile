# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.sln ./
COPY crud-app/*.csproj ./crud-app/
RUN dotnet restore "./crud-app/crud-app.csproj"

COPY . .
WORKDIR /src/crud-app
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "crud-app.dll"]
EXPOSE 8080