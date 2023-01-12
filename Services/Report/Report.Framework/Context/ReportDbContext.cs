using Microsoft.EntityFrameworkCore;
using Report.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Framework.Context
{
	public class ReportDbContext : DbContext
	{
		public ReportDbContext(DbContextOptions<ReportDbContext> dbContextOptions) : base(dbContextOptions)
		{
		}

		public DbSet<ContactReport> ContactReports { get; set; }
	}
}
