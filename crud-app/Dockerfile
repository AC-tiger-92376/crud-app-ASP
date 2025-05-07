# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy .csproj file and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore
EXPOSE 8080
# Copy the rest of the code
COPY . ./

# Build and publish
RUN dotnet publish -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "crud-app.dll"]
