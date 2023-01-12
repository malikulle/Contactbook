using CommonProject.Result;
using CommonProject.ViewModels.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Framework.Services.Abstract
{
    public interface IContactHttpClientService
    {
        Response<List<ContractPersonReport>> GetReport();
    }
}
