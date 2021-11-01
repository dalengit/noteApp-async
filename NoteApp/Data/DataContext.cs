using Microsoft.EntityFrameworkCore;
using NoteApp.Models;

namespace NoteApp.Data
{
    public class DataContext : DbContext
    {
        // Property
        public DbSet<Note> Notes { get; set; }

        //Constructor 
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        // Entity method
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>().HasKey(x => x.Id);
        }
    }
}
