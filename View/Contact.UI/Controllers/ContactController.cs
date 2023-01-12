using CommonProject.Extensions;
using CommonProject.ViewModels.Person;
using Contact.UI.Framework.Models;
using Contact.UI.Framework.Services.Abstarct;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Contact.UI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactHttpClientService _contactHttpClientService;

        public ContactController(IContactHttpClientService contactHttpClientService)
        {
            _contactHttpClientService = contactHttpClientService;
        }

        [HttpGet]
        public IActionResult People()
        {
            var response = _contactHttpClientService.GetPeople();
            return View(response);
        }

        [HttpGet]
        public IActionResult Person(Guid id)
        {
            var response = _contactHttpClientService.GetPerson(id);
            if (response.HasFailed || response.Data is null)
                return RedirectToAction("People");
            return View(response.Data);
        }

        [HttpGet]
        public IActionResult AddPerson()
        {
            CreatePersonViewModel model = new();
            return PartialView("/Views/Shared/Contact/_PersonAddPartial.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPerson(CreatePersonViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _contactHttpClientService.CreatePerson(model);
                if (!result.HasFailed && result.Data != null)
                {
                    var createPersonAjaxViewModel = JsonSerializer.Serialize(new CreatePersonAjaxViewModel()
                    {
                        Data = result.Data,
                        PartialView = await this.RenderViewToStringAsync("/Views/Shared/Contact/_PersonAddPartial.cshtml", model)
                    });
                    return Json(createPersonAjaxViewModel);
                }
            }

            var createPersonAjaxViewErrorModel = JsonSerializer.Serialize(new CreatePersonAjaxViewModel()
            {
                PartialView = await this.RenderViewToStringAsync("/Views/Shared/Contact/_PersonAddPartial.cshtml", model)
            });

            return Json(createPersonAjaxViewErrorModel);
        }

        [HttpPost]
        public IActionResult Person(UpdatePersonViewModel model)
        {
            if (ModelState.IsValid)
            {
                _contactHttpClientService.UpdatePerson(model);
            }
            var person = _contactHttpClientService.GetPerson(model.Id);
            return View(person.Data);
        }

        [HttpGet]
        public IActionResult AddPersonContact(string id)
        {
            CreatePersonContactViewModel model = new();
            model.PersonId = new Guid(id);
            return PartialView("/Views/Shared/Contact/_PersonContactAddPartial.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPersonContact(CreatePersonContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _contactHttpClientService.CreatePersonContact(model);
                if (!result.HasFailed && result.Data != null)
                {
                    var createPersonContactAjaxViewModel = JsonSerializer.Serialize(new CreatePersonContactAjaxViewModel()
                    {
                        Data = result.Data,
                        PartialView = await this.RenderViewToStringAsync("/Views/Shared/Contact/_PersonContactAddPartial.cshtml", model)
                    });
                    return Json(createPersonContactAjaxViewModel);
                }
            }

            var createPersonContactAjaxErrorViewModel = JsonSerializer.Serialize(new CreatePersonContactAjaxViewModel()
            {
                PartialView = await this.RenderViewToStringAsync("/Views/Shared/Contact/_PersonContactAddPartial.cshtml", model)
            });

            return Json(createPersonContactAjaxErrorViewModel);
        }

        [HttpGet]
        public IActionResult UpdatePersonContact(string id)
        {
            UpdatePersonContactViewModel model = new ();
            var result = _contactHttpClientService.GetPersonContact(new Guid(id));
            if(!result.HasFailed && result.Data != null)
			{
                model.Id = result.Data.Id;
                model.Description = result.Data.Description;
                model.ContactType = result.Data.ContactType;
			}
            return PartialView("/Views/Shared/Contact/_PersonContactUpdatePartial.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePersonContact(UpdatePersonContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _contactHttpClientService.UpdatePersonContact(model);
                if (!result.HasFailed && result.Data != null)
                {
                    var createPersonContactAjaxViewModel = JsonSerializer.Serialize(new CreatePersonContactAjaxViewModel()
                    {
                        Data = result.Data,
                        PartialView = await this.RenderViewToStringAsync("/Views/Shared/Contact/_PersonContactUpdatePartial.cshtml", model)
                    });
                    return Json(createPersonContactAjaxViewModel);
                }
            }

            var createPersonContactAjaxErrorViewModel = JsonSerializer.Serialize(new CreatePersonContactAjaxViewModel()
            {
                PartialView = await this.RenderViewToStringAsync("/Views/Shared/Contact/_PersonContactUpdatePartial.cshtml", model)
            });

            return Json(createPersonContactAjaxErrorViewModel);
        }

        [HttpPost]
        public IActionResult DeletePerson(DeletePersonAjaxRequest request)
        {
            var result = _contactHttpClientService.DeletePerson(request.Id);
            return Json(result);
        }

        [HttpPost]
        public IActionResult DeletePersonContact(DeletePersonAjaxRequest request)
        {
            var result = _contactHttpClientService.DeletePersonContact(request.Id);
            return Json(result);
        }
    }
}
