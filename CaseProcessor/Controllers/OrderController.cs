using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using DataModels;
using Microsoft.AspNetCore.Mvc;

namespace CaseProcessor.Controllers
{
    [Route("order")]
    public class OrderController : GenericController<CaseBaseModel>
    {
        public OrderController(IOrderService<CaseBaseModel> orderService) : base(orderService)
        {

        }

    }
}