# Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy and restore
COPY . .
RUN dotnet restore

# Publish projects
RUN dotnet publish Battleships.Domain/Battleships.Domain.csproj -c Release -o /app/out
RUN dotnet publish Battleships.Common/Battleships.Common.csproj -c Release -o /app/out
RUN dotnet publish Battleships.Application/Battleships.Application.csproj -c Release -o /app/out
RUN dotnet publish Battleships.ConsoleUI/Battleships.ConsoleUI.csproj -c Release -o /app/out

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Copy appsettings.json to the runtime image
COPY Battleships.ConsoleUI/appsettings.json ./

ENTRYPOINT ["dotnet", "Battleships.ConsoleUI.dll"]