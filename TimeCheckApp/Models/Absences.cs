using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TimeCheckApp.Models
{
    public class Absences
    {
        public int ID { get; set; }

        [Display(Name = "Absence Hours")]
        public float Hours { get; set; }

        [Display(Name = "Absence Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Absence Type")]
        public string AbsenceType { get; set; }

        public int week { get; set; }

        [ForeignKey("PersonID")]
        public Person Person { get; set; }

        public int PersonID { get; set; }

        public enum AbsencesType
        {
            Holiday,
            SickLeave,
            AdministrativeObligations
        }
    }
}
