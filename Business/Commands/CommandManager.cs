using DataModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Business.Commands
{
    public interface ICommandManager
    {
        Task ResolveCommand<T, DT>(Command command) where T : Command where DT : CaseBaseModel;
    }
    public class CommandManager : ICommandManager
    {
        IServiceProvider services;
        public CommandManager(IServiceProvider services)
        {
            this.services = services;
        }

        public async Task ResolveCommand<T, DT>(Command command) where T: Command where DT: CaseBaseModel
        {
            var resolverObject = this.services.GetService<ICommandResolver<T, DT>>();
            DT data = command.Data as DT;
            await resolverObject.DoWork(data);
        }
    }
}
