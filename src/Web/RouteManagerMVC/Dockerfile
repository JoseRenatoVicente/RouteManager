#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Web/RouteManagerMVC/RouteManagerMVC.csproj", "src/Web/RouteManagerMVC/"]
COPY ["src/BuildingBlocks/RouteManager.Domain.Core/RouteManager.Domain.Core.csproj", "src/BuildingBlocks/RouteManager.Domain.Core/"]
COPY ["src/BuildingBlocks/RouteManager.WebAPI.Core/RouteManager.WebAPI.Core.csproj", "src/BuildingBlocks/RouteManager.WebAPI.Core/"]
RUN dotnet restore "src/Web/RouteManagerMVC/RouteManagerMVC.csproj"
COPY . .
WORKDIR "/src/src/Web/RouteManagerMVC"
RUN dotnet build "RouteManagerMVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RouteManagerMVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RouteManagerMVC.dll"]
