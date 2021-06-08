#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Account/Account.Api/Account.Api.csproj", "Account/Account.Api/"]
COPY ["Account/Account.Business/Account.Business.csproj", "Account/Account.Business/"]
COPY ["Shared/Shared.Helper/Shared.Helper.csproj", "Shared/Shared.Helper/"]
COPY ["Account/Account.Infra/Account.Infra.csproj", "Account/Account.Infra/"]
COPY ["Shared/Shared.Error/Shared.Error.csproj", "Shared/Shared.Error/"]
COPY ["Account/Account.Repository/Account.Repository.csproj", "Account/Account.Repository/"]
COPY ["Account/Account.DataAccess/Account.DataAccess.csproj", "Account/Account.DataAccess/"]
COPY ["Shared/Shared.Constants/Shared.Constants.csproj", "Shared/Shared.Constants/"]
COPY ["Shared/Shared.Audit/Shared.Audit.csproj", "Shared/Shared.Audit/"]
COPY ["Shared/Shared.Extensions/Shared.Extensions.csproj", "Shared/Shared.Extensions/"]
COPY ["Shared/Shared.Messages/Shared.Messages.csproj", "Shared/Shared.Messages/"]
RUN dotnet restore "Account/Account.Api/Account.Api.csproj"
COPY . .
WORKDIR "/src/Account/Account.Api"
RUN dotnet build "Account.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Account.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Account.Api.dll"]