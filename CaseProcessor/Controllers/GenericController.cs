using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using DataModels;
using Microsoft.AspNetCore.Mvc;

namespace CaseProcessor.Controllers
{
    public class GenericController<T> : ControllerBase where T : CaseBaseModel
    {
        protected int CaseCategory;
        IOrderService<T> orderService;

        public GenericController(IOrderService<T> orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Post([FromBody] T request)
        {
            request.Id = Guid.NewGuid().ToString();
            request = await this.orderService.UpdateHisstory(request);
            await this.orderService.SaveOrder(request);

            return Ok(request);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Put([FromBody] T request)
        {
            request = await this.orderService.UpdateHisstory(request);
            await this.orderService.UpdateOrder(request);
            return new OkResult();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await this.orderService.DeleteOrder(id);
            return new OkResult();
        }

        [HttpGet]
        [Route("order/{id}")]
        public async Task<IActionResult> GetOrder(string id)
        {
            var result = await this.orderService.GetOrder(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("orders")]
        public async Task<IActionResult> GetAllOrders(Guid id)
        {
            var result = await this.orderService.GetOrders();
            return Ok(result);
        }
    }
}