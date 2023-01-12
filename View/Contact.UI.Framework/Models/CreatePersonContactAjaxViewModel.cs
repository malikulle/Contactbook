using CommonProject.ViewModels.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.UI.Framework.Models
{
	public class CreatePersonContactAjaxViewModel
	{
		public PersonContactViewModel Data { get; set; }
		public string PartialView { get; set; }
	}
}
