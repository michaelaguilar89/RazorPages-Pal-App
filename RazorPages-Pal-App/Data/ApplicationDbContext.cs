using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RazorPages_Pal_App.Models;
using RazorPages_Pal_App.Dto_s;

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
        public DbSet<RazorPages_Pal_App.Dto_s.ResultStoreDto> ResultStoreDto { get; set; } = default!;
    }
}
