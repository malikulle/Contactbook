using Contact.UI.Framework.Services.Abstarct;
using Microsoft.AspNetCore.Mvc;

namespace Contact.UI.Controllers
{
    public class ReportController : Controller
    {

        private readonly IReportHttpClientService _reportHttpClientService;

        public ReportController(IReportHttpClientService reportHttpClientService)
        {
            _reportHttpClientService = reportHttpClientService;
        }

        [HttpGet]
        public IActionResult List()
        {
            var result = _reportHttpClientService.GetContactReports();
            return View(result);
        }

        [HttpPost]
        public IActionResult RequestReport()
        {
            var result = _reportHttpClientService.RequestReport();
            return Json(result);
        }
    }
}
