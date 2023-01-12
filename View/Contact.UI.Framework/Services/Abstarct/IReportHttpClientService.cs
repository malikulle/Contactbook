using CommonProject.Result;
using CommonProject.ViewModels.ContactReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.UI.Framework.Services.Abstarct
{
    public interface IReportHttpClientService
    {
        Response<List<ContactReportViewModel>> GetContactReports();
        Response<ContactReportViewModel> RequestReport();
    }
}
