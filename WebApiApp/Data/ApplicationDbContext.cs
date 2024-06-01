using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApiApp.Models;

namespace WebApiApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)   
        {
            
        }

        public DbSet<Stock> Stock { get; set; } 
        public DbSet<Comment> Comments {get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Portfolio>(x => x.HasKey(p => new {p.ApplicationUserId, p.StockId}));
            modelBuilder.Entity<Portfolio>()
            .HasOne(u=>u.User)
            .WithMany(u => u.Portfolios)
            .HasForeignKey(u => u.ApplicationUserId);

            modelBuilder.Entity<Portfolio>()
            .HasOne(u=>u.Stock)
            .WithMany(u => u.Portfolios)
            .HasForeignKey(u => u.StockId);
            
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}