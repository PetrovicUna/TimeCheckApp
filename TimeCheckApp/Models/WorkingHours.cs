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

        public int week { get; set; }

        public string Status { get; set; }

        public string Comment { get; set; }

        public float Hours { get; set; }

        [Display(Name = "Booking Type")]
        public string BookingType { get; set; }

        [ForeignKey("PersonID")]
        public Person Person { get; set; }

        [Display(Name = "Person")]
        public int PersonID { get; set; }
        
        [Display(Name = "Task")]
        [ForeignKey("TaskID")]
        public Tasks Tasks { get; set; }

        [Display(Name = "Task")]
        public int TaskID { get; set; }


        public enum BookingTypes
        {
            Regular,
            Overtime,
            ExtendedOvertime
        }

        public enum Statuses
        {
            Approved,
            Saved,
            Submmited
        }
    }
}
