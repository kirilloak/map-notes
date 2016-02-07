using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MapNotes.DAL.Entities;
using MapNotes.DAL.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MapNotes.DAL
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext() : base("name=DefaultConnection", false)
        {
        }

        public static DataContext Create()
        {
            return new DataContext();
        }

        public DbSet<NoteEntity> Note { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
