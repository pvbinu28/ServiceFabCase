using DataModels;
using DataModels.CaseModel;
using Microsoft.Extensions.Configuration;
using Microsoft.ServiceFabric.Services.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.Fabric.Query;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;

namespace UIService.Resolver
{
    public interface IProcessResolver
    {
        Task<string> GetServiceBaseUrlAsync(int key, CancellationToken token);
        string UrlResolver<T>(T model) where T : CaseBaseModel;
    }
    public class ProcessorResolver : IProcessResolver
    {
        private readonly ServicePartitionResolver resolver = ServicePartitionResolver.GetDefault();
        private readonly IConfiguration configuration;

        public ProcessorResolver(IConfiguration config)
        {
            configuration = config;
        }


        public async Task<string> GetServiceBaseUrlAsync(int key, CancellationToken token)
        {
            var urlString = configuration.GetValue<string>("FabricUri");
            var url = new Uri(urlString);
            var partitionKey = new ServicePartitionKey(key);
            var resolver = ServicePartitionResolver.GetDefault();
            var partition = await resolver.ResolveAsync(url, partitionKey, token);
            JObject addresses = JObject.Parse(partition.GetEndpoint().Address);
            string primaryReplicaAddress = (string)addresses["Endpoints"].First();
            return primaryReplicaAddress;
        }

        public string UrlResolver<T>(T model) where T: CaseBaseModel
        {
            var controllerName = "";
            switch(model)
            {
                case FraudCaseModel _:
                    controllerName = CaseCategories.Case_Route_Fraud;
                    break;
                case TrafficLightModel _:
                    controllerName = CaseCategories.Case_Rout_TrafficLight;
                    break;
            }

            return controllerName;
        }
    }
}
