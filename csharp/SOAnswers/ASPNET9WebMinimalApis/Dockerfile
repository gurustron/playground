FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ASPNET9WebMinimalApis/ASPNET9WebMinimalApis.csproj", "ASPNET9WebMinimalApis/"]
RUN dotnet restore "ASPNET9WebMinimalApis/ASPNET9WebMinimalApis.csproj"
COPY . .
WORKDIR "/src/ASPNET9WebMinimalApis"
RUN dotnet build "ASPNET9WebMinimalApis.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ASPNET9WebMinimalApis.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ASPNET9WebMinimalApis.dll"]
