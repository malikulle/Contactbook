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
        public IActionResult AddPerson()
        {
            var model = new CreatePersonViewModel();
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
        public IActionResult DeletePerson(DeletePersonAjaxRequest request)
        {
            var result = _contactHttpClientService.DeletePerson(request.Id);
            return Json(result);
        }
    }
}
