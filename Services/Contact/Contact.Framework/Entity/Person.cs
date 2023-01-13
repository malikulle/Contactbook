using CommonProject.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contact.Framework.Entity
{
    [Table(nameof(Person), Schema = "Contact")]
    public class Person : BaseEntity
    {
        /// <summary>
        ///  Person Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Person Surname
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Person Compnay
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// Relational Object
        /// </summary>
        public List<PersonContact> PersonContacts { get; set; }
    }
}
