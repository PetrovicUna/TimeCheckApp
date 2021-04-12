using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace TimeCheckApp.Models
{
    public class ProjectTask
    {
        [Key]
        public int ID { get; set; }

        public Projects Project { get; set; }

        public int ProjectID { get; set; }

        public Tasks Tasks { get; set; }

        public int TaskID { get; set; }
    }
}
