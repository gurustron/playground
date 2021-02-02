# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
#RUN apt update && apt install default-jdk -y --no-install-recommends
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.csproj ./ignite-k8s-asp/
WORKDIR /source/ignite-k8s-asp
RUN dotnet restore

# copy everything else and build app
COPY . .
RUN dotnet publish -c release -o /app 

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
## Workaround for apt/jre issues
RUN mkdir -p /usr/share/man/man1
RUN apt update && apt install apt-utils -y --no-install-recommends

## Install JRE
RUN apt update && apt install default-jre -y --no-install-recommends
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "ignite-k8s-asp.dll"]