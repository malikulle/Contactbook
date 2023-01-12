using CommonProject.Entity;
using CommonProject.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Framework.Entity
{
    /// <summary>
    /// Person Contact Information
    /// </summary>
    [Table(nameof(PersonContact), Schema = "Contact")]
    public class PersonContact : BaseEntity
    {
        /// <summary>
        /// Contact Type
        /// </summary>
        public ContactType ContactType { get; set; }

        /// <summary>
        /// Content
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Person
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// Relational Object
        /// </summary>
        public virtual Person Person { get; set; }
    }
}
