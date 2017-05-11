using data.Contenus;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.Projects
{
    [Table("Project")]
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
       
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Contenu> Contenus { get; set; }
    }
}
