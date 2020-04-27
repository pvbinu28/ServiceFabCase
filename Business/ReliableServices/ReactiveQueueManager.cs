using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Business.ReliableServices
{
   public interface IReactiveReliableQueueManager
    {
        IReactiveReliableQueue<T> GetOrCreateAsync<T>(IReliableQueue<T> queue);
    }

    public class ReactiveReliableQueueManager : IReactiveReliableQueueManager
    {
        private readonly ConcurrentDictionary<Uri, object> reactiveReliableQueues = new ConcurrentDictionary<Uri, object>();
        public IReactiveReliableQueue<T> GetOrCreateAsync<T>(IReliableQueue<T> queue)
        {
            var wrappedQueues = reactiveReliableQueues.GetOrAdd(queue.Name, x => new ReactiveReliableQueue<T>(queue));
            return (IReactiveReliableQueue<T>)wrappedQueues;
        }
    }

    public static class ReliableStateManagerExtensions
    {
        private static readonly IReactiveReliableQueueManager reactiveQueueManager = new ReactiveReliableQueueManager();
        public static async Task<IReactiveReliableQueue<T>> GetOrAddReactiveReliableQueue<T>(this IReliableStateManager stateManager, string name)
        {
            var queue = await stateManager.GetOrAddAsync<IReliableQueue<T>>(name).ConfigureAwait(false);
            return reactiveQueueManager.GetOrCreateAsync(queue);
        }
    }
}
