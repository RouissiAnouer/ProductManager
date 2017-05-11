using data.Projects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.Contenus
{
    [Table("Contenu")]
    public class Contenu
    {
        [Key]
        public int ContenuId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        
        
    }
}
