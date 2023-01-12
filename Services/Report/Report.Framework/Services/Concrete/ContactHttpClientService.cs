using CommonProject.Http;
using CommonProject.Result;
using CommonProject.ViewModels.Person;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Report.Framework.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Framework.Services.Concrete
{
    public class ContactHttpClientService : IContactHttpClientService
    {
        private readonly APISettings _settings;

        public ContactHttpClientService(IOptions<APISettings> options)
        {
            _settings = options.Value;
        }

        public Response<List<ContractPersonReport>> GetReport()
        {
            return MakeRequest<List<ContractPersonReport>>(HttpMethod.Get, _settings.ContactAPI + "contact/report", new { });
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
