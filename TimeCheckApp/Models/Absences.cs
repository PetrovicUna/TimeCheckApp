using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCheckApp.Models
{
    public class Absences
    {
        public int ID { get; set; }

        public int PersonID { get; set; }

        public float Hours { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        enum AbsenceType
        {
            Holiday,
            SickLeave,
            AdministrativeObligations
        }

        public ICollection<PersonAbsences> PersonAbsences { get; set; }
    }
}
