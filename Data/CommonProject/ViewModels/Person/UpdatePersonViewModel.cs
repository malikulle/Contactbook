﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonProject.ViewModels.Person
{
	public class UpdatePersonViewModel
	{
		public Guid Id { get; set; }
		
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Company is required.")]
        public string Company { get; set; }
    }
}
