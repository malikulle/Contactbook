using CommonProject.Http;
using CommonProject.Result;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.WorkerService.Services
{
    public class ReportHttpClientService
    {
        private readonly APISettings _settings;

        public ReportHttpClientService(IOptions<APISettings> options)
        {
            _settings = options.Value;
        }

        public Response<bool> DoneRequest(Guid Id)
        {
            return MakeRequest<bool>(HttpMethod.Post, _settings.ReportAPI + "Report/contactReport/doneRequest", new { Id });
        }

        private Response<T> MakeRequest<T>(HttpMethod method, string URL, object data)
        {
            var response = new Response<T>();
            var result = HttpClientHelper.RequestAsJson(method, URL, data);
            if (result.IsSuccessStatusCode)
                response = JsonConvert.DeserializeObject<Response<T>>(result.Content.ReadAsStringAsync().Result);
            else
                response.Fail("ServiceERROR");
            return response;
        }
    }
}
