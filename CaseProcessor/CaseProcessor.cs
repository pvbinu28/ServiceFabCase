using System;
using System.Collections.Generic;
using System.Fabric;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Services.Communication.AspNetCore;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Microsoft.ServiceFabric.Data;
using Business.ReliableServices;
using DataModels;
using Business.Commands;

namespace CaseProcessor
{
    /// <summary>
    /// The FabricRuntime creates an instance of this class for each service type instance. 
    /// </summary>
    internal sealed class CaseProcessor : StatefulService
    {
        public IServiceProvider provider;
        public CaseProcessor(StatefulServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// Optional override to create listeners (like tcp, http) for this service instance.
        /// </summary>
        /// <returns>The collection of listeners.</returns>
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new ServiceReplicaListener[]
            {
                new ServiceReplicaListener(serviceContext =>
                    new KestrelCommunicationListener(serviceContext, (url, listener) =>
                    {
                        ServiceEventSource.Current.ServiceMessage(serviceContext, $"Starting Kestrel on {url}");
                        var host = new WebHostBuilder()
                                    .UseKestrel()
                                    .UseCommonConfiguration()
                                    .ConfigureServices(
                                        services => services
                                            .AddSingleton<StatefulServiceContext>(serviceContext)
                                            .AddSingleton<IReliableStateManager>(this.StateManager))
                                    .UseContentRoot(Directory.GetCurrentDirectory())
                                    .UseStartup<Startup>()
                                    .UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.UseUniqueServiceUrl)
                                    .UseUrls(url)
                                    .Build();
                        // This is added to access the service collection from RunAsync method.
                        provider = host.Services;
                        return host;
                    }))
            };
        }

        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            var queue = await StateManager.GetOrAddReactiveReliableQueue<Command>("case");
            while (true)
            {
                if (provider == null)
                {
                    continue;
                }

                cancellationToken.ThrowIfCancellationRequested();
                using (var tx = StateManager.CreateTransaction())
                {
                    var result = await queue.TryDequeueAsync(tx, cancellationToken);
                    if (result.HasValue)
                    {
                        var ct = result.Value.GetType();
                        var dt = result.Value.Data.GetType();
                        var commandManager = provider.GetService<ICommandManager>();
                        try
                        {
                            var method = typeof(ICommandManager).GetMethod(nameof(ICommandManager.ResolveCommand));
                            var generic = method.MakeGenericMethod(ct, dt);
                            var task = (Task)generic.Invoke(commandManager, new[] { result.Value });
                            await task;
                        }
                        catch (Exception ex)
                        {
                            ServiceEventSource.Current.ServiceMessage(Context, $"Failed to invoke method : {ex}");
                        }

                    }

                    await tx.CommitAsync();
                }
            }
        }
    }
}
