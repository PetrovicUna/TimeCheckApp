using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCheckApp.Models
{
    public class Projects
    {
        public int ID { get; set; }

        public int ProjectCode { get; set; }

        public string ProjectName { get; set; }

        public string Client { get; set; }

        public ICollection<ProjectTask> ProjectTasks  { get; set; }
    }
}
