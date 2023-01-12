using CommonProject.Entity;
using CommonProject.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Framework.Entity
{
	[Table(nameof(ContactReport), Schema = "Report")]
	public class ContactReport : BaseEntity
	{
		/// <summary>
		/// Date
		/// </summary>
		public DateTime ReportDate { get; set; }

		/// <summary>
		/// ReportType, Done
		/// </summary>
		public ReportType ReportType { get; set; }

		/// <summary>
		/// Report File Path
		/// </summary>
		public string FilePath { get; set; }
	}
}
