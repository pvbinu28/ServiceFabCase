using Business.Commands;
using Business.Interfaces;
using Business.ReliableServices;
using DataModels;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business
{
    public class OrderService<T> : IOrderService<T> where T: CaseBaseModel
    {
        IReliableStateManager stateManager;
        const string CollectionName = "orders";
        public OrderService(IReliableStateManager stateManager)
        {
            this.stateManager = stateManager;
        }

        public async Task DeleteOrder(string id)
        {
            var collection = await this.stateManager.GetOrAddAsync<IReliableDictionary<string, T>>(CollectionName);
            using(ITransaction tx = this.stateManager.CreateTransaction())
            {
                await collection.TryRemoveAsync(tx, id);
                DeleteCommand<T> saveCommand = new DeleteCommand<T>();
                var data = new CaseBaseModel { Id = id };
                saveCommand.Data = data;
                var saveQueue = await stateManager.GetOrAddReactiveReliableQueue<Commands.Command>("case");
                await saveQueue.EnqueueAsync(tx, saveCommand);
                await tx.CommitAsync();
            }
        }

        public Task<List<T>> GetOrders()
        {
            throw new NotImplementedException();
        }

        public async Task SaveOrder(T request)
        {
            var collection = await this.stateManager.GetOrAddAsync<IReliableDictionary<string, CaseBaseModel>>(CollectionName);
            using (ITransaction tx = this.stateManager.CreateTransaction())
            {
                await collection.AddAsync(tx, request.Id, request);
                SaveCommand<T> saveCommand = new SaveCommand<T>();
                saveCommand.Data = request;
                var saveQueue = await stateManager.GetOrAddReactiveReliableQueue<Commands.Command>("case");
                await saveQueue.EnqueueAsync(tx, saveCommand);
                await tx.CommitAsync();
            }
        }

        public async Task UpdateOrder(T request)
        {
            var collection = await this.stateManager.GetOrAddAsync<IReliableDictionary<string, T>>(CollectionName);
            using (ITransaction tx = this.stateManager.CreateTransaction())
            {
                await collection.AddOrUpdateAsync(tx, request.Id, request, (id, oilValue) => request);
                UpdateCommand<T> saveCommand = new UpdateCommand<T>();
                saveCommand.Data = request;
                var saveQueue = await stateManager.GetOrAddReactiveReliableQueue<Commands.Command>("case");
                await saveQueue.EnqueueAsync(tx, saveCommand);
                await tx.CommitAsync();
            }
        }

        public Task<T> UpdateHisstory(T order)
        {
            if (order.History == null)
            {
                order.History = new List<DataModels.History>();
            }

            var lastStatus = order.History.OrderByDescending(item => item.StatusDate).FirstOrDefault();
            if (lastStatus != null)
            {
                if (lastStatus.StatusId != order.StatusId)
                {
                    order.History.Add(new DataModels.History()
                    {
                        Status = order.Status,
                        StatusId = order.StatusId,
                        StatusDate = DateTime.Now
                    });
                }
            }
            else
            {
                order.History.Add(new DataModels.History()
                {
                    Status = order.Status,
                    StatusId = order.StatusId,
                    StatusDate = DateTime.Now
                });
            }

            return Task.FromResult<T>(order);
        }

        public async Task<T> GetOrder(string id)
        {
            ConditionalValue<T> result;
            var collection = await this.stateManager.GetOrAddAsync<IReliableDictionary<string, T>>(CollectionName);
            using (ITransaction tx = this.stateManager.CreateTransaction())
            {
                result = await collection.TryGetValueAsync(tx, id);
                await tx.CommitAsync();
            }

            if(result.HasValue)
            {
                return result.Value;
            } else
            {
                return null;
            }
        }
    }
}
