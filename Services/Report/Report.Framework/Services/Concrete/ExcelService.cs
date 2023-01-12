using Report.Framework.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using CommonProject.Extensions;
using CommonProject.Excel;

namespace Report.Framework.Services.Concrete
{
    public class ExcelService : IExcelService
    {
        private readonly IHostEnvironment _environment;
        private readonly IContactHttpClientService _contactHttpClientService;

        public ExcelService(IContactHttpClientService contactHttpClientService, IHostEnvironment environment)
        {
            _contactHttpClientService = contactHttpClientService;
            _environment = environment;
        }

        public string ExportExcel(Guid reportId)
        {
            var result = _contactHttpClientService.GetReport();
            if (!result.HasFailed)
            {
                var path = "/Files/" + reportId + ".xlsx";
                var exactPath = _environment.ContentRootPath + "wwwroot" + path;
                var dataTable = result.Data.ToDataTable();
                ExcelHelper.CreateExcelFile(dataTable, exactPath);
                return path;
            }
            return "";
        }
    }
}
