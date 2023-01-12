﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonProject.ViewModels.Person
{
    public class PersonContactViewModel
    {
        public Guid Id { get; set; }
        public int ContactType { get; set; }
        public string Description { get; set; }
        public Guid PersonId { get; set; }
    }
}
