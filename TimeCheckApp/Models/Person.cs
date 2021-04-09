
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace TimeCheckApp.Models
{
    public class Person
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public int PersonNumber { get; set; }

        public string Country { get; set; }
        public string WorkLocation { get; set; }


        [ForeignKey("GradeID")] 
        public Grades Grades { get; set; }
        public int GradeID { get; set; }


        public ICollection<WorkingHours> WorkingHours { get; set; }

        public ICollection<PersonAbsences> PersonAbsences { get; set; }
    }
    //dodati country u bazu
        public enum Country
        {
            RS,
            SI
        }

}
