using AutoMapper;
using CommonProject.Entity;
using CommonProject.Enum;
using CommonProject.Http;
using CommonProject.Result;
using CommonProject.ViewModels.ContactReport;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Report.Framework.Context;
using Report.Framework.Entity;
using Report.Framework.RabbitMQ;
using Report.Framework.Services.Abstract;

namespace Report.Framework.Services.Concrete
{
    public class ContactReportService : IContactReportService
    {
        private readonly ReportDbContext _context;
        private readonly RabbitMQPublisher _rabbitMQPublisher;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;
        private readonly APISettings _settings;


        public ContactReportService(ReportDbContext context, IMapper mapper, IExcelService excelService, IOptions<APISettings> options, RabbitMQPublisher rabbitMQPublisher)
        {
            _context = context;
            _mapper = mapper;
            _excelService = excelService;
            _settings = options.Value;
            _rabbitMQPublisher = rabbitMQPublisher;
        }

        public async Task<Response<bool>> DoneRequest(Guid Id)
        {
            var response = new Response<bool>();
            var entity = _context.ContactReports.Where(op => op.Id == Id).FirstOrDefault();
            if (entity != null && string.IsNullOrEmpty(entity.FilePath))
            {
                entity.ReportType = ReportType.Done;
                var path = _excelService.ExportExcel(Id);
                entity.FilePath = path;
                await _context.SaveChangesAsync();
                response.SetData(true);
            }
            return response;
        }

        public async Task<Response<List<ContactReportViewModel>>> GetContactReports()
        {
            var response = new Response<List<ContactReportViewModel>>();
            var list = await _context.ContactReports.AsNoTracking().Where(op => op.Status != Status.Deleted).OrderByDescending(op => op.ReportDate).ToListAsync();
            var mappedList = _mapper.Map<List<ContactReportViewModel>>(list);
            response.SetData(mappedList);
            foreach (var item in response.Data)
            {
                if (!string.IsNullOrEmpty(item.FilePath))
                    item.FilePath = _settings.CDNBasePath + item.FilePath;
            }
            return response;
        }

        public async Task<Response<ContactReportViewModel>> RequestReport()
        {
            var response = new Response<ContactReportViewModel>();
            ContactReport entity = new();
            entity.ReportDate = DateTime.UtcNow;
            entity.ReportType = ReportType.Preparing;
            entity.Status = Status.Active;
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            var mappedEntity = _mapper.Map<ContactReportViewModel>(entity);
            response.SetData(mappedEntity);
            // Send To Rabbit MQ
            _rabbitMQPublisher.Publish(mappedEntity);
            return response;
        }
    }
}
