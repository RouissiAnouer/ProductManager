using data.Contenus;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.Configuration
{
    public class ContenuConfiguration :EntityTypeConfiguration<Contenu>
    {
        public ContenuConfiguration()
        {
            HasRequired(t => t.Project)
                .WithMany(p => p.Contenus)
                .HasForeignKey(c => c.ProjectId)
                .WillCascadeOnDelete(true);
        }

    }
}
