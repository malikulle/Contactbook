using CommonProject.Result;
using CommonProject.ViewModels.ContactReport;

namespace Report.Framework.Services.Abstract
{
	public interface IContactReportService
	{
		Task<Response<List<ContactReportViewModel>>> GetContactReports();
		Task<Response<ContactReportViewModel>> RequestReport();
		Task<Response<bool>> DoneRequest(Guid Id);
	}
}
