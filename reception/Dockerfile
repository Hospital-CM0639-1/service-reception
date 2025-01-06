FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["reception/reception.csproj", "reception/"]
COPY ["DAL.Psql.Migrations/DAL.Psql.Migrations.csproj", "DAL.Psql.Migrations/"]
COPY ["DataAccessLayer/DataAccessLayer.csproj", "DataAccessLayer/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "reception/reception.csproj"
COPY . .
WORKDIR "/src/reception"
RUN dotnet build "reception.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "reception.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM ubuntu AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN apt-get update && apt-get install -y iputils-ping
ENTRYPOINT ["dotnet", "reception.dll"]
