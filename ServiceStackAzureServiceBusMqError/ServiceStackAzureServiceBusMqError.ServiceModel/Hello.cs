using ServiceStack;

namespace ServiceStackAzureServiceBusMqError.ServiceModel
{
    [Route("/hello/{Name}")]
    public class Hello
    {
        public string Name { get; set; }
    }
}