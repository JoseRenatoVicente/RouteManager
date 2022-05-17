#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0-alpine AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["src/Services/Logging/Logging.Consumer/Logging.Consumer.csproj", "src/Services/Logging/Logging.Consumer/"]
RUN dotnet restore "src/Services/Logging/Logging.Consumer/Logging.Consumer.csproj"
COPY . .
WORKDIR "/src/src/Services/Logging/Logging.Consumer"
RUN dotnet build "Logging.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Logging.Consumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Logging.Consumer.dll"]
