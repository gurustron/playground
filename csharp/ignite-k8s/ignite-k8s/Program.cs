using System;
using Apache.Ignite.Core;
using Apache.Ignite.Core.Discovery.Tcp;
using Apache.Ignite.Core.Discovery.Tcp.Static;

var ignite = Ignition.Start(new IgniteConfiguration
{
    DiscoverySpi = new TcpDiscoverySpi
    {
        IpFinder = new TcpDiscoveryStaticIpFinder
        {
            Endpoints = new []{"127.0.0.1:42500..42509", "127.0.0.1:44500..44509"}
        }
    }
});
var clusterNodes = ignite.GetCluster().GetNodes();
Console.WriteLine("SEARCHSTRING   " + clusterNodes.Count);
Console.ReadLine();
Ignition.Stop(ignite.Name, true);