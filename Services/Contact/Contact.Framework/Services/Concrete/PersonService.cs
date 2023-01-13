using AutoMapper;
using CommonProject.Entity;
using CommonProject.Enum;
using CommonProject.Result;
using CommonProject.ViewModels.Person;
using Contact.Framework.Context;
using Contact.Framework.Entity;
using Contact.Framework.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Contact.Framework.Services.Concrete
{
    public class PersonService : IPersonService
    {
        private readonly ContactDbContext _context;
        private readonly IMapper _mapper;

        public PersonService(ContactDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<PersonViewModel>> CreatePerson(CreatePersonViewModel person)
        {
            var response = new Response<PersonViewModel>();
            Person entity = new();
            entity.Status = Status.Active;
            entity.Name = person.Name;
            entity.Surname = person.Surname;
            entity.Company = person.Company;
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            var model = _mapper.Map<PersonViewModel>(entity);
            response.SetData(model);
            return response;
        }

        public async Task<Response<PersonContactViewModel>> CreatePersonContact(CreatePersonContactViewModel personContact)
        {
            var response = new Response<PersonContactViewModel>();
            PersonContact entity = new();
            entity.PersonId = personContact.PersonId;
            entity.Status = Status.Active;
            entity.Description = personContact.Description;
            entity.ContactType = personContact.ContactType;
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            var model = _mapper.Map<PersonContactViewModel>(entity);
            response.SetData(model);
            return response;
        }

        public async Task<Response<bool>> DeletePerson(Guid id)
        {
            var response = new Response<bool>();
            var entity = _context.People.Where(op => op.Id == id).FirstOrDefault();
            if (entity is null)
            {
                response.Fail("Person Not Found");
                return response;
            }

            entity.Status = Status.Deleted;
            await _context.SaveChangesAsync();
            response.SetData(true);
            return response;
        }

        public async Task<Response<bool>> DeletePersonContact(Guid id)
        {
            var response = new Response<bool>();
            var entity = _context.PersonContacts.Where(op => op.Id == id).FirstOrDefault();
            if (entity is null)
            {
                response.Fail("Detail Not Found");
                return response;
            }

            entity.Status = Status.Deleted;
            await _context.SaveChangesAsync();
            response.SetData(true);
            return response;
        }

        public async Task<Response<List<PersonViewModel>>> GetPeople()
        {
            var response = new Response<List<PersonViewModel>>();
            var list = await _context.People.Where(op => op.Status != Status.Deleted).OrderByDescending(op => op.Id).ToListAsync();
            var mappedList = _mapper.Map<List<PersonViewModel>>(list);
            response.SetData(mappedList);
            return response;
        }

        public async Task<Response<PersonViewModel>> GetPerson(Guid id)
        {
            var response = new Response<PersonViewModel>();
            var entity = await _context.People.Where(op => op.Id == id).Include(op => op.PersonContacts.Where(x => x.Status != Status.Deleted)).FirstOrDefaultAsync();
            var mappedObject = _mapper.Map<PersonViewModel>(entity);
            response.SetData(mappedObject);
            return response;
        }

        public async Task<Response<PersonContactViewModel>> GetPersonContact(Guid id)
        {
            var response = new Response<PersonContactViewModel>();
            var entity = await _context.PersonContacts.Where(op => op.Id == id).FirstOrDefaultAsync();
            var mappedObject = _mapper.Map<PersonContactViewModel>(entity);
            response.SetData(mappedObject);
            return response;
        }

        public Response<List<ContractPersonReport>> GetReport()
        {
            var response = new Response<List<ContractPersonReport>>();
            var list = _context.PersonContacts
                .Where(op => op.ContactType == ContactType.Location)
                .GroupBy(op => op.Description)
                .Select(op => new ContractPersonReport()
                {
                    Location = op.Key,
                    LocationCount = op.Count()
                }).ToList();

            foreach (var item in list)
            {
                item.LocationPersonCount = _context.People.Count(op => op.PersonContacts.Any(x => x.Description == item.Location));
            }
            response.SetData(list);
            return response;
        }

        public async Task<Response<PersonViewModel>> UpdatePerson(UpdatePersonViewModel person)
        {
            var response = new Response<PersonViewModel>();
            var entity = await _context.People.Where(op => op.Id == person.Id).FirstOrDefaultAsync();
            if (entity != null)
            {
                entity.Name = person.Name;
                entity.Surname = person.Surname;
                entity.Company = person.Company;
                await _context.SaveChangesAsync();
                var mappedObject = _mapper.Map<PersonViewModel>(entity);
                response.SetData(mappedObject);
            }
            return response;
        }

        public async Task<Response<PersonContactViewModel>> UpdatePersonContact(UpdatePersonContactViewModel personContact)
        {
            var response = new Response<PersonContactViewModel>();
            var entity = await _context.PersonContacts.Where(op => op.Id == personContact.Id).FirstOrDefaultAsync();
            if (entity != null)
            {
                entity.ContactType = personContact.ContactType;
                entity.Description = personContact.Description;
                await _context.SaveChangesAsync();
                var mappedObject = _mapper.Map<PersonContactViewModel>(entity);
                response.SetData(mappedObject);
            }
            return response;
        }
    }
}