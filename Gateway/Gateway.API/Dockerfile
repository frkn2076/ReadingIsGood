#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Gateway/Gateway.Api/Gateway.Api.csproj", "Gateway/Gateway.Api/"]
COPY ["Shared/Shared.Extensions/Shared.Extensions.csproj", "Shared/Shared.Extensions/"]
RUN dotnet restore "Gateway/Gateway.Api/Gateway.Api.csproj"
COPY . .
WORKDIR "/src/Gateway/Gateway.Api"
RUN dotnet build "Gateway.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gateway.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gateway.Api.dll"]