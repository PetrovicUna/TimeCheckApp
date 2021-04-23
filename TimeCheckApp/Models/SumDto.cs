using System.ComponentModel.DataAnnotations;

namespace TimeCheckApp.Models
{
    public class SumDto
    {
        public int PersonID { get; set; }

        [Display(Name = "Person Name")]
        public string PersonName { get; set; }

        [Display(Name = "ExtendedOvertime Hours")]
        public float ExtendedOvertimeHours { get; set; }

        [Display(Name = "Overtime Hours")]
        public float OvertimeHours { get; set; }

        [Display(Name = "Regular Hours")]
        public float RegularHours { get; set; }

        [Display(Name = "Absence Hours")]
        public float AbsenceHours { get; set; }

        public string Status { get; set; }
    }
}
