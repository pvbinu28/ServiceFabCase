using Business.Interfaces;
using DataModels;
using DataModels.CaseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIService.Resolver;

namespace UIService.Controllers
{
    [ApiController]
    [Route("api/" + CaseCategories.Case_Route_Fraud)]
    public class FraudController: GenericController<FraudCaseModel>
    {
        public FraudController(IProcessInvoker invoker, IDataAccess<FraudCaseModel> dataAccess) : base(invoker, dataAccess)
        {
            CaseCategory = CaseCategories.CitizenIssues;
            CurrentCaseType = CaseType.Fraud;
        }
    }

    [ApiController]
    [Route("api/" + CaseCategories.Case_Rout_TrafficLight)]
    public class TrafficLightController : GenericController<TrafficLightModel>
    {
        public TrafficLightController(IProcessInvoker invoker, IDataAccess<TrafficLightModel> dataAccess) : base(invoker, dataAccess)
        {
            CaseCategory = CaseCategories.HighWayIssues;
            CurrentCaseType = CaseType.TrafficLight;
        }
    }
}
