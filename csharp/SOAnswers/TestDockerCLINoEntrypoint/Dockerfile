FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
RUN dotnet new console --use-program-main -o . -n MyApp
RUN cat <<EOF > Program.cs
namespace MyApp;

class Program
{
    static void Main(string[] args)
    {
        foreach (var arg in args)
        {
            Console.WriteLine($"This from env: ({Environment.GetEnvironmentVariable("CONNECTION_STRING")})");
            Console.WriteLine(arg);
        }
    }
}
EOF
RUN dotnet publish -o /app

FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /app .
