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
        
        public ServiceInfo getInfo(string serviceName)
        {
            switch (serviceName)
            {
                case "calculator":
                    return buildServiceInfo("localhost", 10002);
            }
            ServiceNotFoundException serviceNotFoundException = new ServiceNotFoundException();
            serviceNotFoundException.Message = "Service '" + serviceName + "' not found"; 
            throw serviceNotFoundException;
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
