using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonProject.Enum
{
    public enum ContactType : byte
    {
        None = 0,
        [Display(Name = "Mobile Phone")]
        MobilePhone = 1,
        
        [Display(Name = "Email")]
        Email = 2,

        [Display(Name = "Location")]
        Location = 3
    }
}
