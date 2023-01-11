using Microsoft.EntityFrameworkCore;
using PicturyMarket.Domain.Entity;

namespace PicturyMarket.DAL
{
    public class PicturyMarketDbContext : DbContext
    {
        public PicturyMarketDbContext(DbContextOptions<PicturyMarketDbContext> options) : base(options) 
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Pictury> Picturies { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}
