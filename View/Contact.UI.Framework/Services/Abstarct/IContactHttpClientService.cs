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
        Response<PersonViewModel> GetPerson(Guid id);
        Response<PersonViewModel> CreatePerson(CreatePersonViewModel personViewModel);
        Response<PersonViewModel> UpdatePerson(UpdatePersonViewModel personViewModel);
        Response<bool> DeletePerson(Guid id);
        Response<PersonContactViewModel> GetPersonContact(Guid id);
        Response<PersonContactViewModel> CreatePersonContact(CreatePersonContactViewModel model);
        Response<PersonContactViewModel> UpdatePersonContact(UpdatePersonContactViewModel model);
        Response<bool> DeletePersonContact(Guid id);

    }
}
