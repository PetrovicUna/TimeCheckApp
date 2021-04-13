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

        public string Date { get; set; }

        public string Status { get; set; }

        public string Comment { get; set; }

        public float Hours { get; set; }

        public string BookingType { get; set; }

        [ForeignKey("PersonID")]
        public Person Person { get; set; }
        public int PersonID { get; set; }

        [ForeignKey("TaskID")]
        public Tasks Tasks { get; set; }

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
