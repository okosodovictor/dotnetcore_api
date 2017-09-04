using System;

namespace VacationSolution.Web.Entities
{
    public class VacationRequest
    {
        public int RequestID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int NumberOfDays { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public string CalendarEventID { get; set; }
        public int DaysRemaining { get; set; }
        public int ApprovalID { get; set; }
        public virtual User User { get; set; }


    }
}
