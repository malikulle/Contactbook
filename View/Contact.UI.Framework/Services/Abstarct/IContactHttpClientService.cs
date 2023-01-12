using CommonProject.Result;
using CommonProject.ViewModels.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.UI.Framework.Services.Abstarct
{
    public interface IContactHttpClientService
    {
        Response<List<PersonViewModel>> GetPeople();
        Response<PersonViewModel> CreatePerson(CreatePersonViewModel personViewModel);
        Response<bool> DeletePerson(Guid id);
    }
}
