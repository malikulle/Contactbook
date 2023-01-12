using CommonProject.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonProject.ViewModels.ContactReport
{
	public class ContactReportViewModel
	{
		public Guid Id { get; set; }
		public DateTime ReportDate { get; set; }
		public ReportType ReportType { get; set; }
		public string FilePath { get; set; }
		public string ReportDateString
        {
			get
            {
				return ReportDate.ToString("dd-MM-yyyy HH:ss");
            }
        }
		public string ReportTypeString
        {
			get
            {
				return ReportType.ToString();
            }
        }
	}
}
