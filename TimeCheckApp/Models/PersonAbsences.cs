using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCheckApp.Models
{
    public class PersonAbsences
    {
        public int ID { get; set; }

        [ForeignKey("PersonID")]
        public Person Person { get; set; }
        public int PersonID { get; set; }


        [ForeignKey("AbsenceID")]
        public Absences Absences { get; set; }
        public int AbsenceID { get; set; }
    }
}
