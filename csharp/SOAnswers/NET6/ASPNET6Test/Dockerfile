FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ASPNET6Test/ASPNET6Test.csproj", "ASPNET6Test/"]
COPY ["NET6LibTest/NET6LibTest.csproj", "NET6LibTest/"]
RUN dotnet restore "ASPNET6Test/ASPNET6Test.csproj"
COPY . .
WORKDIR "/src/ASPNET6Test"
RUN dotnet build "ASPNET6Test.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ASPNET6Test.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ASPNET6Test.dll"]
