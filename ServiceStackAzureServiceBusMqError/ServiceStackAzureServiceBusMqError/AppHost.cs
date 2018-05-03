using System;
using System.Threading;
using Funq;
using ServiceStack;
using ServiceStack.Azure.Messaging;
using ServiceStack.Messaging;
using ServiceStackAzureServiceBusMqError.ServiceInterface;
using ServiceStackAzureServiceBusMqError.ServiceModel;

namespace ServiceStackAzureServiceBusMqError
{
    //VS.NET Template Info: https://servicestack.net/vs-templates/EmptyAspNet
    public class AppHost : AppHostBase
    {
        /// <summary>
        /// Base constructor requires a Name and Assembly where web service implementation is located
        /// </summary>
        public AppHost()
            : base("ServiceStackAzureServiceBusMqError", typeof(MyServices).Assembly) { }

        /// <summary>
        /// Application specific configuration
        /// This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        public override void Configure(Container container)
        {
            //Config examples
            //this.Plugins.Add(new PostmanFeature());
            //this.Plugins.Add(new CorsFeature());

            QueueNames.MqPrefix = "";

            var connectionString = "";

            var client = new ServiceBusMqMessageFactory(connectionString).CreateMessageQueueClient();
            client.Publish(new Hello
            {
                Name = "George"
            });

            container.Register<IMessageService>(c => new ServiceBusMqServer(connectionString)
            {
                RetryCount = 0
            });

            var mqServer = container.Resolve<IMessageService>();
            mqServer.RegisterHandler<Hello>(ExecuteMessage);

            mqServer.Start();

            Thread.Sleep(TimeSpan.FromSeconds(5)); // Simulate a more complicated app with more plugins, filters, etc.
        }
    }
}
