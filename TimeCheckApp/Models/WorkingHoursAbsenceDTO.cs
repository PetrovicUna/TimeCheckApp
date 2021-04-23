using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCheckApp.Models
{
    public class WorkingHoursAbsenceDTO
    {
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public float Hours { get; set; }

        public int week { get; set; }

        public string BookingType { get; set; }

        public string AbsenceType { get; set; }

        public float AbsenceHours { get; set; }
    }
}
