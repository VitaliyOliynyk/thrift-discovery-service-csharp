using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thrift.discoveryservice;

namespace thrift_discovery_service_csharp
{
    public class DiscoveryServiceHandler : thrift.discoveryservice.DiscoveryService.Iface
    {
        IList<short> usedPorts = new List<short>();
        IDictionary<string, ServiceInfo> serviceInfos = new Dictionary<string, ServiceInfo>();

        public DiscoveryServiceHandler()
        {
            serviceInfos["calculator"] = buildServiceInfo("localhost", makeRandomPort());
        }


        public ServiceInfo getInfo(string serviceName)
        {
            if (serviceInfos.ContainsKey(serviceName)) 
            {
                return serviceInfos[serviceName];
            }
            ServiceNotFoundException serviceNotFoundException = new ServiceNotFoundException();
            serviceNotFoundException.Message = "Service '" + serviceName + "' not found"; 
            throw serviceNotFoundException;
        }

        private short makeRandomPort()
        {
            Random random = new Random();
            short port;
            lock(this) {
                do
                {
                    port = (short) random.Next(10002, 20000);
                } while (usedPorts.Contains((short)port));
                usedPorts.Add(port);
            }
            return port;       
        }

        private ServiceInfo buildServiceInfo(string host, short port)
        {
            ServiceInfo serviceInfo = new ServiceInfo();
            serviceInfo.Host = host;
            serviceInfo.Port = port;
            return serviceInfo;
        }
    }
}
