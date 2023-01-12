
namespace CommonProject.ViewModels.Person
{
    public class PersonViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public List<PersonContactViewModel> PersonContacts { get; set; }
    }
}
