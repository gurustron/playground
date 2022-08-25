using System;
using System.Linq;
using Apache.Ignite.Core;
using Apache.Ignite.Core.Discovery.Tcp;
using Apache.Ignite.Core.Discovery.Tcp.Static;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace ignite_k8s_asp
{
    
    public class Program
    {
        public static IIgnite Ignite;

        public static void Main(string[] args)
        {
            var configurationRoot = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json", optional: true)
                .Build();
            var endpoints = configurationRoot.GetSection("IgniteEndpoints").Get<string[]>() ?? new string[0];
            Console.WriteLine($"SEST: {string.Join(",", endpoints)}");
            
            Ignite = endpoints.Any()
                ? Ignition.Start(
                    new IgniteConfiguration
            {
                // DiscoverySpi = new TcpDiscoverySpi
                // {
                //     IpFinder = new TcpDiscoveryStaticIpFinder
                //     {
                //         Endpoints = new []{"127.0.0.1:42500..42509", "127.0.0.1:44500..44509"}
                //     }
                // }
                        DiscoverySpi = new TcpDiscoverySpi
                        {
                            IpFinder = new TcpDiscoveryStaticIpFinder
                            {
                                Endpoints = endpoints
                            }
                        }
                    }
                )
                : Ignition.Start();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}