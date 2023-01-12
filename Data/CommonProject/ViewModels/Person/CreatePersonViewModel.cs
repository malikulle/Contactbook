using System.ComponentModel.DataAnnotations;

namespace CommonProject.ViewModels.Person
{
    public class CreatePersonViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Company is required.")]
        public string Company { get; set; }
    }
}
