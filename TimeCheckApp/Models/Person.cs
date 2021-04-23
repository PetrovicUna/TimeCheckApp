
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

        public string GradeCode { get; set; }

        public string GradeName { get; set; }

        public ICollection<WorkingHours> WorkingHours { get; set; }

        public ICollection<Absences> Absences { get; set; }

    }
    //dodati country u bazu
        public enum Country
        {
            RS,
            SI
        }

}
