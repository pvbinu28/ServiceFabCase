using Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using DataModels;
using System.Threading.Tasks;

namespace Business
{
    public class DataAccess<T>: IDataAccess<T> where T: CaseBaseModel
    {
        private readonly IMongoCollection<T> collection;

        public DataAccess(IDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var db = client.GetDatabase(settings.DatabaseName);
            collection = db.GetCollection<T>(settings.OrderCollectionName);
        }

        public async Task<List<T>> Get(CaseType currentCaseType)
        {
            var result = await collection.FindAsync(order => order.CaseTypeId == (int)currentCaseType);
            return await result.ToListAsync();
        }

        public async Task<T> Get(string id)
        {
            var result = await collection.FindAsync(order => order.Id == id);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<T> Create(T order)
        {
            await collection.InsertOneAsync(order);
            return order;
        }

        public async Task Update(T order)
        {
            await collection.ReplaceOneAsync(od => od.Id == order.Id, order);
        }

        public async Task Delete(string id)
        {
            await collection.DeleteOneAsync(od => od.Id == id);
        }
    }
}
