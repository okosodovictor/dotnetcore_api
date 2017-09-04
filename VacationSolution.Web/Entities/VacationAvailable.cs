using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VacationSolution.Web.Entities
{
    public class VacationAvailable
    {
        [ForeignKey("User"), Key]
        public int UserID { get; set; }
        public int DaysAvalibale { get; set; }
        public virtual User User { get; set; }
    }
}
