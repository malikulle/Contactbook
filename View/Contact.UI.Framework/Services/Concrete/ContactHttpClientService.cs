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
            var response = new Response<PersonViewModel>();
            var URL = _settings.ContactAPI + "Contact/person";
            var result = HttpClientHelper.RequestAsJson(HttpMethod.Post, URL, personViewModel);
            if (result.IsSuccessStatusCode)
                response = JsonConvert.DeserializeObject<Response<PersonViewModel>>(result.Content.ReadAsStringAsync().Result);
            else
                response.Fail("ServiceERROR");
            return response;
        }

        public Response<bool> DeletePerson(Guid id)
        {
            var response = new Response<bool>();
            var URL = _settings.ContactAPI + "Contact/person/" + id;
            var result = HttpClientHelper.RequestAsJson(HttpMethod.Delete, URL, new { });
            if (result.IsSuccessStatusCode)
                response = JsonConvert.DeserializeObject<Response<bool>>(result.Content.ReadAsStringAsync().Result);
            else
                response.Fail("ServiceERROR");
            return response;
        }

        public Response<List<PersonViewModel>> GetPeople()
        {
            var response = new Response<List<PersonViewModel>>();
            var URL = _settings.ContactAPI + "Contact/personList";
            var result = HttpClientHelper.RequestAsJson(HttpMethod.Get, URL, new { });
            if (result.IsSuccessStatusCode)
                response = JsonConvert.DeserializeObject<Response<List<PersonViewModel>>>(result.Content.ReadAsStringAsync().Result);
            else
                response.Fail("ServiceERROR");
            return response;
        }
    }
}
