using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCheckApp.Models
{
    public class Grades
    {
        [Key]
        public int GradeCode { get; set; }

        public string GradeName { get; set; }

        public Person Person { get; set; }
    }
}
