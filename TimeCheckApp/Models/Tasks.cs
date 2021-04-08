using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCheckApp.Models
{
    public class Tasks
    {
        public int ID { get; set; }

        public int TaskNumber { get; set; }

        public string TaskName { get; set; }

        public string FM { get; set; }

        public string Activity { get; set; }

        public ICollection<WorkingHours> WorkingHours { get; set; }

        public ICollection<ProjectTask> ProjectTasks { get; set; }

    }

}
