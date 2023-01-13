using CommonProject.Result;
using CommonProject.ViewModels.ContactReport;
using Moq;
using Report.API.Controllers;
using Report.Framework.Services.Abstract;
using System;
using System.Collections.Generic;
using Xunit;

namespace Report.Test
{
    public class ReportControllerTest
    {
        private readonly Mock<IContactReportService> _service;
        private readonly ReportController _controller;

        public ReportControllerTest()
        {
            _service = new Mock<IContactReportService>();
            _controller = new ReportController(_service.Object);
        }

        [Fact]
        public async void GetContactReports_ReturnList()
        {
            var response = new Response<List<ContactReportViewModel>>();
            response.Data = new List<ContactReportViewModel>();
            _service.Setup(x => x.GetContactReports()).ReturnsAsync(response);
            var result = await _controller.GetContactReports();
            Assert.Equal(response.Data,result.Data);
        }

        [Fact]
        public async void RequestReport_ReturnDataNotNull()
        {
            var response = new Response<ContactReportViewModel>();
            response.Data = new ContactReportViewModel();
            _service.Setup(x => x.RequestReport()).ReturnsAsync(response);
            var result = await _controller.RequestReport();
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async void DoneRequest_ReturnDataTrue()
        {
            var id = Guid.NewGuid();
            var response = new Response<bool>();
            response.Data =true;
            _service.Setup(x => x.DoneRequest(id)).ReturnsAsync(response);
            var result = await _controller.DoneRequest(new DoneRequestModel() { Id = id });
            Assert.True(result.Data);
        }
    }
}