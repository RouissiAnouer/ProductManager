using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using data.Projects;
using data.Security;
using data.Contenus;

namespace data
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("DefaultConnection")
        {
            
        }
        
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Contenu> Contenus { get; set; }
    

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contenu>()
            .HasRequired(t => t.Project)
            .WithMany()
            .HasForeignKey(c => c.ProjectId)
            .WillCascadeOnDelete(true);
    } 
}
}