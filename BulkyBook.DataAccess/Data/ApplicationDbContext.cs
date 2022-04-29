using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.DataAccess
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        //whatever the tables we want to add to our database we have to add it here .. and give it a proper name. 
        public DbSet<Category> Categories { get; set;}

    }
}
