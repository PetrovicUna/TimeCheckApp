using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace TimeCheckApp.Models
{
    public class ProjectTask
    {
        public  int ID { get; set; }

        [ForeignKey("ProjectID")]
        public Projects Project { get; set; }

        public int ProjectID { get; set; }

        [ForeignKey("TaskID")]
        public Tasks Tasks { get; set; }

        public int TaskID { get; set; }
    }
}
