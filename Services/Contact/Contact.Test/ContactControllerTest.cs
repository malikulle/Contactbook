using CommonProject.Result;
using CommonProject.ViewModels.Person;
using Contact.API.Controllers;
using Contact.Framework.Services.Abstract;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Contact.Test
{
    public class ContactControllerTest
    {
        private readonly Mock<IPersonService> _service;
        private readonly ContactController _controller;

        public ContactControllerTest()
        {
            _service = new Mock<IPersonService>();
            _controller = new ContactController(_service.Object);
        }

        [Fact]
        public void GetReport_ReturnResponseOk()
        {
            _service.Setup(x => x.GetReport()).Returns(new Response<List<ContractPersonReport>>());
            var result = _controller.GetReport();
            Assert.False(result.HasFailed);
        }

        [Fact]
        public async void GetPeople_ReturnResponseOk()
        {
            _service.Setup(x => x.GetPeople()).ReturnsAsync(new Response<List<PersonViewModel>>());
            var result = await _controller.GetPeople();
            Assert.IsType<Response<List<PersonViewModel>>>(result);
        }

        [Fact]
        public async void GetPerson_ReturnNull()
        {
            var id = Guid.NewGuid();
            _service.Setup(x => x.GetPerson(id)).ReturnsAsync(new Response<PersonViewModel>());
            var result = await _controller.GetPerson(id);
            Assert.Null(result.Data);
        }

        [Fact]
        public async void CreatePerson_ReturnPersonNotNull()
        {
            var person = new CreatePersonViewModel()
            {
                Company = "test",
                Name = "test",
                Surname = "test"
            };
            var response = new Response<PersonViewModel>();
            response.Data = new PersonViewModel()
            {
                Company = person.Company,
                Name = person.Name,
                Surname = person.Surname
            };
            _service.Setup(x => x.CreatePerson(person)).ReturnsAsync(response);
            var result = await _controller.CreatePerson(person);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async void UpdatePerson_ReturnPersonIdSame()
        {
            var person = new UpdatePersonViewModel()
            {
                Id = Guid.NewGuid(),
                Company = "test",
                Name = "test",
                Surname = "test"
            };
            var response = new Response<PersonViewModel>();
            response.Data = new PersonViewModel()
            {
                Id = person.Id,
                Company = person.Company,
                Name = person.Name,
                Surname = person.Surname
            };
            _service.Setup(x => x.UpdatePerson(person)).ReturnsAsync(response);
            var result = await _controller.UpdatePerson(person);
            Assert.Equal(person.Id, result.Data.Id);
        }

        [Fact]
        public async void DeletePerson_ReturnDataTrue()
        {
            var id = Guid.NewGuid();
            var response = new Response<bool>();
            response.Data = true;
            _service.Setup(x => x.DeletePerson(id)).ReturnsAsync(response);
            var result = await _controller.DeletePerson(id);
            Assert.True(response.Data);
        }

        [Fact]
        public async void GetPersonContact_ReturnDataNotNull()
        {
            var id = Guid.NewGuid();
            var response = new Response<PersonContactViewModel>();
            response.Data = new PersonContactViewModel();
            _service.Setup(x => x.GetPersonContact(id)).ReturnsAsync(response);
            var result = await _controller.GetPersonContact(id);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async void CreatePersonContact_ReturnGuid()
        {
            var contactPerson = new CreatePersonContactViewModel()
            {
                ContactType = CommonProject.Enum.ContactType.Location,
                Description = "TEST",
                PersonId = Guid.NewGuid(),
            };

            var response = new Response<PersonContactViewModel>();
            response.Data = new PersonContactViewModel()
            {
                Id = Guid.NewGuid(),
                PersonId = contactPerson.PersonId,
                Description = contactPerson.Description,
                ContactType = contactPerson.ContactType,
            };

            _service.Setup(x => x.CreatePersonContact(contactPerson)).ReturnsAsync(response);
            var result = await _controller.CreatePersonContact(contactPerson);
            Assert.IsType<Guid>(result.Data.Id);
        }

        [Fact]
        public async void UpdatePersonContact_ReturnPersonIdSame()
        {
            var contactPerson = new UpdatePersonContactViewModel()
            {
                ContactType = CommonProject.Enum.ContactType.Email,
                Description = "TEST",
                Id = Guid.NewGuid(),
            };
            var response = new Response<PersonContactViewModel>();
            response.Data = new PersonContactViewModel()
            {
                Id = contactPerson.Id,
            };
            _service.Setup(x => x.UpdatePersonContact(contactPerson)).ReturnsAsync(response);
            var result = await _controller.UpdatePersonContact(contactPerson);
            Assert.Equal(result.Data.Id,contactPerson.Id);
        }
        
        [Fact]
        public async void DeletePersonContact_ReturnDataTrue()
        {
            var id = Guid.NewGuid();
            var response = new Response<bool>();
            response.Data = true;
            _service.Setup(x => x.DeletePersonContact(id)).ReturnsAsync(response);
            var result =  await _controller.DeletePersonContact(id);
            Assert.True(response.Data);
        }

    }
}