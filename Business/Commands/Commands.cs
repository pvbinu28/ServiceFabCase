using DataModels;
using DataModels.CaseModel;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Business.Commands
{
    [DataContract]
    [KnownType(typeof(SaveCommand<CaseBaseModel>))]
    [KnownType(typeof(SaveCommand<FraudCaseModel>))]
    public class Command
    {
        [DataMember]
        public CaseBaseModel Data { get; set; }
    }

    public interface ICommandResolver<T, DT> where T: Command where DT: CaseBaseModel
    {
        Task DoWork(DT data);
    }

}
