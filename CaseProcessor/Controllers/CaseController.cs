using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using DataModels.CaseModel;
using Microsoft.AspNetCore.Mvc;

namespace CaseProcessor.Controllers
{
    [Route("fraud")]
    public class FraudController : GenericController<FraudCaseModel>
    {
        public FraudController(IOrderService<FraudCaseModel> orderService) : base(orderService)
        {
        }
    }

    [Route("trafficlight")]
    public class TrafficLightController : GenericController<TrafficLightModel>
    {
        public TrafficLightController(IOrderService<TrafficLightModel> orderService) : base(orderService)
        {
        }
    }
}