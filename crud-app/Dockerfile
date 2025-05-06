# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln ./
COPY . ./
RUN dotnet restore

# Copy the rest of the source code
COPY . ./
WORKDIR /app/crud-app
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/crud-app/out ./
ENTRYPOINT ["dotnet", "MyApi.dll"]
