using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Business.Interfaces;
using DataModels;
using Microsoft.AspNetCore.Mvc;
using UIService.Resolver;

namespace UIService.Controllers
{
    public class GenericController<T> : ControllerBase where T: CaseBaseModel
    {
        private readonly IProcessInvoker invoker;
        private readonly IDataAccess<T> dataAccess;
        protected int CaseCategory;
        protected CaseType CurrentCaseType;
        public GenericController(IProcessInvoker invoker, IDataAccess<T> dataAccess)
        {
            this.invoker = invoker;
            this.dataAccess = dataAccess;
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Post([FromBody] T request, CancellationToken token)
        {
            await invoker.InvokeSaveProcess<T>(request, CaseCategory, token);
            return Ok(request);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Put([FromBody] T request)
        {
            return new OkResult();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return new OkResult();
        }

        [HttpGet]
        [Route("case/{id}")]
        public async Task<IActionResult> GetCase(string id)
        {
            return Ok();
        }

        [HttpGet]
        [Route("cases")]
        public async Task<IActionResult> GetCases()
        {
            var result = await dataAccess.Get(this.CurrentCaseType);
            return Ok(result);
        }
    }
}