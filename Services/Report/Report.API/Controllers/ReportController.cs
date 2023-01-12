using CommonProject.Result;
using CommonProject.ViewModels.ContactReport;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Report.Framework.Services.Abstract;

namespace Report.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IContactReportService _contactReportService;

        public ReportController(IContactReportService contactReportService)
        {
            _contactReportService = contactReportService;
        }

        [HttpGet("contactReport")]
        public async Task<Response<List<ContactReportViewModel>>> GetContactReports()
        {
            return await _contactReportService.GetContactReports();
        }

        [HttpPost("contactReport/requestReport")]
        public async Task<Response<ContactReportViewModel>> RequestReport()
        {
            return await _contactReportService.RequestReport();
        }

        [HttpPost("contactReport/doneRequest")]
        public async Task<Response<bool>> DoneRequest([FromBody] DoneRequestModel requestModel)
        {
            return await _contactReportService.DoneRequest(requestModel.Id);
        }
    }
}
