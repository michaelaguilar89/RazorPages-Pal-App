using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RazorPages_Pal_App.Models;

namespace RazorPages_Pal_App.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Production> productions { get; set; }

        public DbSet<Store> stores { get; set; }

        public DbSet<StoreHistory> storeHistories { get; set; }
    }
}
