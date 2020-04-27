using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataModels;

namespace Business.Interfaces
{
    public interface IOrderService<T> where T: CaseBaseModel
    {
        Task SaveOrder(T request);

        Task UpdateOrder(T request);

        Task DeleteOrder(string id);

        Task<T> GetOrder(string id);

        Task<List<T>> GetOrders();

        Task<T> UpdateHisstory(T order);
    }
}
