using ServiceStack;
using ServiceStackAzureServiceBusMqError.ServiceModel;

namespace ServiceStackAzureServiceBusMqError.ServiceInterface
{
    public class MyServices : Service
    {
        public object Any(Hello request)
        {
            return null;
        }
    }
}