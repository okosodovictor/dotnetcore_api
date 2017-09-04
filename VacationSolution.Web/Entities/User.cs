using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VacationSolution.Web.Entities
{
    public class User
    {
     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual VacationAvailable VacationAvailable { get; set; }
        public virtual ICollection<VacationRequest> VacationRequest { get; set; }
    }
}
