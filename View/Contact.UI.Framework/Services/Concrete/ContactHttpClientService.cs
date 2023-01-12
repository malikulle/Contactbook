using CommonProject.Http;
using CommonProject.Result;
using CommonProject.ViewModels.Person;
using Contact.UI.Framework.Services.Abstarct;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Contact.UI.Framework.Services.Concrete
{
    public class ContactHttpClientService : IContactHttpClientService
    {
        private readonly APISettings _settings;

        public ContactHttpClientService(IOptions<APISettings> options)
        {
            _settings = options.Value;
        }
        public Response<PersonViewModel> CreatePerson(CreatePersonViewModel personViewModel)
        {
            return MakeRequest<PersonViewModel>(HttpMethod.Post, _settings.ContactAPI + "Contact/person", personViewModel);
        }

        public Response<PersonContactViewModel> CreatePersonContact(CreatePersonContactViewModel model)
        {
            return MakeRequest<PersonContactViewModel>(HttpMethod.Post, _settings.ContactAPI + "Contact/personContact", model);
        }

        public Response<bool> DeletePerson(Guid id)
        {
            return MakeRequest<bool>(HttpMethod.Delete, _settings.ContactAPI + "Contact/person/" + id, new { });
        }

        public Response<bool> DeletePersonContact(Guid id)
        {
            return MakeRequest<bool>(HttpMethod.Delete, _settings.ContactAPI + "Contact/personContact/" + id, new { });
        }

        public Response<List<PersonViewModel>> GetPeople()
        {
            return MakeRequest<List<PersonViewModel>>(HttpMethod.Get, _settings.ContactAPI + "Contact/personList", new { });
        }

        public Response<PersonViewModel> GetPerson(Guid id)
        {
            return MakeRequest<PersonViewModel>(HttpMethod.Get, _settings.ContactAPI + "Contact/person/" + id, new { });
        }

		public Response<PersonContactViewModel> GetPersonContact(Guid id)
		{
            return MakeRequest<PersonContactViewModel>(HttpMethod.Get, _settings.ContactAPI + "Contact/personContact/" + id, new { });
        }

        public Response<PersonViewModel> UpdatePerson(UpdatePersonViewModel personViewModel)
		{
            return MakeRequest<PersonViewModel>(HttpMethod.Put, _settings.ContactAPI + "Contact/person", personViewModel);
        }

		public Response<PersonContactViewModel> UpdatePersonContact(UpdatePersonContactViewModel model)
		{
            return MakeRequest<PersonContactViewModel>(HttpMethod.Put, _settings.ContactAPI + "Contact/personContact", model);
        }

        private Response<T> MakeRequest<T>(HttpMethod method, string URL, object data)
        {
            var response = new Response<T>();
            var result = HttpClientHelper.RequestAsJson(method, URL, data);
            if (result.IsSuccessStatusCode)
                response = JsonConvert.DeserializeObject<Response<T>>(result.Content.ReadAsStringAsync().Result);
            else
                response.Fail("ServiceERROR");
            return response;
        }
    }
}
