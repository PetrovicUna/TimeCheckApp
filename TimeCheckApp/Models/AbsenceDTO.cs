using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCheckApp.Models
{
    public class AbsenceDTO
    {
        [Display(Name = "Absence Hours")]
        public float Hours { get; set; }

        [Display(Name = "Absence Type")]
        public string AbsenceType { get; set; }

        public int week { get; set; }

        public Person Person { get; set; }

        public int PersonID { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}
