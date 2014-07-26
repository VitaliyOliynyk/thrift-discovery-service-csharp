using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thrift.discoveryservice;
using Thrift.Transport;
using Thrift.Server;

namespace thrift_discovery_service_csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            short portNumber = 10001;
            if (args.Length > 1)
            {
                syntax();
                return;
            }

            if (args.Length == 1) 
            {
                try
                {
                    portNumber = short.Parse(args[0]);
                }
                catch (Exception)
                {
                    syntax();
                    return;
                }
            }
 

            DiscoveryService.Iface handler = new DiscoveryServiceHandler();
            DiscoveryService.Processor processor = new DiscoveryService.Processor(handler);
            TServerTransport serverTransport = new TServerSocket(portNumber);
            TServer server = new TThreadPoolServer(processor, serverTransport);
            Console.WriteLine("Starting the server...");
            server.Serve();
        }

        private static void syntax()
        {
            Console.WriteLine("syntax: <program name> <port number>");
        }
    }
}
