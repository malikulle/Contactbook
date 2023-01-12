using AutoMapper;
using CommonProject.ViewModels.ContactReport;
using Report.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Framework.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<ContactReport, ContactReportViewModel>().ReverseMap();
		}
	}
}
