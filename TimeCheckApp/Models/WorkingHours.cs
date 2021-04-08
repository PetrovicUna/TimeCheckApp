using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCheckApp.Models
{
    public class WorkingHours
    {
        public int ID { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        enum Status
        {
            Approved,
            Saved,
            Submmited
        }

        public string Comment { get; set; }

        public float Hours { get; set; }

        enum BookingType
        {
            Regular,
            Overtime,
            ExtendedOvertime
        }

        [ForeignKey("PersonID")]
        public Person Person { get; set; }
        public int PersonID { get; set; }

        [ForeignKey("TaskID")]
        public Tasks Tasks { get; set; }

        public int TaskID { get; set; }

 

    }
}
