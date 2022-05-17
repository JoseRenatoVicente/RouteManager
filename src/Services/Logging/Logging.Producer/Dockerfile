#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Logging/Logging.Producer/Logging.Producer.csproj", "src/Services/Logging/Logging.Producer/"]
RUN dotnet restore "src/Services/Logging/Logging.Producer/Logging.Producer.csproj"
COPY . .
WORKDIR "/src/src/Services/Logging/Logging.Producer"
RUN dotnet build "Logging.Producer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Logging.Producer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Logging.Producer.dll"]
