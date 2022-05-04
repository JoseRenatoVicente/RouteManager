#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["src/ApiGateway/ApiGateway.Ocelot/ApiGateway.Ocelot.csproj", "src/ApiGateway/ApiGateway.Ocelot/"]
RUN dotnet restore "src/ApiGateway/ApiGateway.Ocelot/ApiGateway.Ocelot.csproj"
COPY . .
WORKDIR "/src/src/ApiGateway/ApiGateway.Ocelot"
RUN dotnet build "ApiGateway.Ocelot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiGateway.Ocelot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiGateway.Ocelot.dll"]