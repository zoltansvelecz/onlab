# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copy solution and project files
COPY ../*.sln ./
COPY ./*.csproj ./Server/

# Restore dependencies
WORKDIR /src/Server
RUN dotnet restore

# Copy the rest of the Server project
COPY . ./

# Publish the application
RUN dotnet publish -c Release -o /app/out

# Stage 2: Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app
COPY --from=build /app/out ./

# Environment configuration
ENV ASPNETCORE_URLS=http://+:5130
ENV ASPNETCORE_ENVIRONMENT=Development

EXPOSE 5130

ENTRYPOINT ["dotnet", "Server.dll"]


