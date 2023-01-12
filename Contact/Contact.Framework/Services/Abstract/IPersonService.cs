using CommonProject.Result;
using CommonProject.ViewModels.Person;

namespace Contact.Framework.Services.Abstract
{
    public interface IPersonService
    {
        Task<Response<List<PersonViewModel>>> GetPeople();
        Task<Response<PersonViewModel>> GetPerson(Guid id);
        Task<Response<PersonViewModel>> CreatePerson(CreatePersonViewModel person);
        Task<Response<PersonViewModel>> UpdatePerson(UpdatePersonViewModel person);
        Task<Response<bool>> DeletePerson(Guid id);
        Task<Response<PersonContactViewModel>> GetPersonContact(Guid id);
        Task<Response<PersonContactViewModel>> CreatePersonContact(CreatePersonContactViewModel personContact);
        Task<Response<PersonContactViewModel>> UpdatePersonContact(UpdatePersonContactViewModel personContact);
        Task<Response<bool>> DeletePersonContact(Guid id);
        Response<List<ContractPersonReport>> GetReport();
    }
}
