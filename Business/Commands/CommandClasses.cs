using Business.Interfaces;
using DataModels;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Business.Commands
{
    #region Save

    [DataContract]
    public class SaveCommand<T>: Command where T: CaseBaseModel
    {
       
    }

    public class SaveCommandResolver<T,DT>: ICommandResolver<T,DT> where T: Command where DT: CaseBaseModel
    {
        readonly IDataAccess<DT> repo;
        public SaveCommandResolver(IDataAccess<DT> dataRepo)
        {
            repo = dataRepo;
        }

        public async Task DoWork(DT data)
        {
            await Task.Run(() => repo.Create(data));
        }
    }
    #endregion

    #region Update 
    public class UpdateCommand<T>: Command where T: CaseBaseModel
    {
       
    }

    public class UpdateCommandResolver<T, DT> : ICommandResolver<T, DT> where T : Command where DT: CaseBaseModel
    {
        readonly IDataAccess<DT> repo;
        public UpdateCommandResolver(IDataAccess<DT> dataRepo)
        {
            repo = dataRepo;
        }

        public async Task DoWork(DT data)
        {
            await repo.Update(data);
        }
    }
    #endregion

    #region Delete
    public class DeleteCommand<T> : Command where T : CaseBaseModel
    {
    }

    public class DeleteCommandResolver<T, DT> : ICommandResolver<T, DT> where T : Command where DT: CaseBaseModel
    {
        readonly IDataAccess<DT> repo;
        public DeleteCommandResolver(IDataAccess<DT> dataRepo)
        {
            repo = dataRepo;
        }

        public async Task DoWork(DT data)
        {
            await repo.Delete(data.Id);
        }
    }
    #endregion

}
