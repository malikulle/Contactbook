using AutoMapper;
using CommonProject.ViewModels.Person;
using Contact.Framework.Entity;

namespace Contact.Framework.Mapping
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PersonContact, PersonContactViewModel>().ReverseMap();
            CreateMap<Person, PersonViewModel>().ReverseMap();
        }
    }
}
