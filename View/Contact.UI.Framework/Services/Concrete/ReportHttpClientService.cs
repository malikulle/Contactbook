using CommonProject.Http;
using CommonProject.Result;
using CommonProject.ViewModels.ContactReport;
using Contact.UI.Framework.Services.Abstarct;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.UI.Framework.Services.Concrete
{
    public class ReportHttpClientService : IReportHttpClientService
    {
        private readonly APISettings _settings;

        public ReportHttpClientService(IOptions<APISettings> options)
        {
            _settings = options.Value;
        }

        public Response<List<ContactReportViewModel>> GetContactReports()
        {
            return MakeRequest<List<ContactReportViewModel>>(HttpMethod.Get, _settings.ReportAPI + "Report/contactReport", new { });
        }

        public Response<ContactReportViewModel> RequestReport()
        {
            return MakeRequest<ContactReportViewModel>(HttpMethod.Post, _settings.ReportAPI + "Report/contactReport/requestReport", new { });
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
