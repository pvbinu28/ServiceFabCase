using DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UIService.Resolver
{
    public interface IProcessInvoker
    {
        Task InvokeSaveProcess<T>(T request, int key, CancellationToken token) where T : CaseBaseModel;
        Task InvokeUpdateProcess<T>(T request, int key, CancellationToken token) where T : CaseBaseModel;

        Task InvokeDeleteProcess(string request, int key, CancellationToken token);
        Task<T> InvokeGetCase<T>(string id, int key, CancellationToken token) where T : CaseBaseModel;
    }

    public class ProcessInvoker : IProcessInvoker
    {
        private readonly IProcessResolver resolver;
        private readonly HttpClient httpClient;
        public ProcessInvoker(IProcessResolver resolver, HttpClient httpClient)
        {
            this.resolver = resolver;
            this.httpClient = httpClient;
        }
        public Task InvokeDeleteProcess(string request, int key, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<T> InvokeGetCase<T>(string id, int key, CancellationToken token) where T : CaseBaseModel
        {
            throw new NotImplementedException();
        }

        public async Task InvokeSaveProcess<T>(T request, int key, CancellationToken token) where T : CaseBaseModel
        {
            var baseUrl = await resolver.GetServiceBaseUrlAsync(key, token);
            var caseType = resolver.UrlResolver(request);
            var fullUrl = $"{baseUrl}/{caseType}/save";
            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync(fullUrl, data, token);
        }

        public Task InvokeUpdateProcess<T>(T request, int key, CancellationToken token) where T : CaseBaseModel
        {
            throw new NotImplementedException();
        }
    }
}
