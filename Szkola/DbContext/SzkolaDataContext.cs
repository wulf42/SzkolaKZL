using Microsoft.EntityFrameworkCore;
using Szkola.Models;

namespace Szkola.Data
{
    public class SzkolaDataContext : DbContext
    {
        public SzkolaDataContext(DbContextOptions<SzkolaDataContext> options) :
            base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<ClassTeacher> ClassTeachers { get; set; }
    }
}