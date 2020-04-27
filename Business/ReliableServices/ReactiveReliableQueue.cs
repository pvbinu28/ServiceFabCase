using Business.Commands;
using DataModels;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.ReliableServices
{
    public interface IReactiveReliableQueue<T>
    {
        Task EnqueueAsync(ITransaction tx, T item);
        Task<ConditionalValue<T>> TryDequeueAsync(ITransaction tx, CancellationToken token);
    }
    public class ReactiveReliableQueue<T> : IReactiveReliableQueue<T>
    {
        private readonly IReliableQueue<T> queue;
        private readonly SemaphoreSlim signal;

        public ReactiveReliableQueue(IReliableQueue<T> queue)
        {
            this.queue = queue;
            signal = new SemaphoreSlim(1);
        }

        public async Task EnqueueAsync(ITransaction tx, T item)
        {
            await queue.EnqueueAsync(tx, item);
            signal.Release();
        }

        public async Task<ConditionalValue<T>> TryDequeueAsync(ITransaction tx, CancellationToken token)
        {
            await signal.WaitAsync(token).ConfigureAwait(false);
            var result = await queue.TryDequeueAsync(tx).ConfigureAwait(false);
            var countDiff = await GetCountDiff(tx);
            if(countDiff > 0)
            {
                signal.Release(countDiff);
            }

            return result;
        }

        private async Task<int> GetCountDiff(ITransaction tx)
        {
            return (int)await queue.GetCountAsync(tx).ConfigureAwait(false) - signal.CurrentCount;
        }
    }
}
