# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copy solution and project files
COPY ../*.sln ./
COPY ./*.csproj ./AI/

# Restore dependencies
WORKDIR /src/AI
RUN dotnet restore

# Copy the rest of the AI project
COPY . ./

# Publish the app
RUN dotnet publish -c Release -o /app/out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/out ./

# Ensure model file is included
#COPY --from=build /src/AI/Resources ./Resources

COPY --from=build /src/AI/Native ./Native

ENV LD_LIBRARY_PATH=/app/Native:$LD_LIBRARY_PATH


ENV PATH="$PATH:/app/Native"
ENV ASPNETCORE_URLS=http://+:5555
ENV ASPNETCORE_ENVIRONMENT=Development

EXPOSE 5555

ENTRYPOINT ["dotnet", "Ai.dll"]

