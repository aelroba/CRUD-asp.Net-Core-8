using Microsoft.EntityFrameworkCore;
using WebApiApp.Models;

namespace WebApiApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)   
        {
            
        }

        public DbSet<Stock> Stock { get; set; } 
        public DbSet<Comment> Comments {get; set; }
    }
}