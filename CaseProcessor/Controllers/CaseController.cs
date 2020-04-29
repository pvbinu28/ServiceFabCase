using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using DataModels;
using DataModels.CaseModel;
using Microsoft.AspNetCore.Mvc;

namespace CaseProcessor.Controllers
{
    [Route(CaseCategories.Case_Route_Fraud)]
    public class FraudController : GenericController<FraudCaseModel>
    {
        public FraudController(IOrderService<FraudCaseModel> orderService) : base(orderService)
        {
        }
    }

    [Route(CaseCategories.Case_Rout_TrafficLight)]
    public class TrafficLightController : GenericController<TrafficLightModel>
    {
        public TrafficLightController(IOrderService<TrafficLightModel> orderService) : base(orderService)
        {
        }
    }
}