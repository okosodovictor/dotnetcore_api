using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VacationSolution.Web.Entities
{
    public class Profile
    {
        [ForeignKey("User"), Key]
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Picture { get; set; }
        public virtual User User { get; set; }
    }
}
