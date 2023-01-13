using Microsoft.EntityFrameworkCore;
using PicturyMarket.Domain.Entity;

namespace PicturyMarket.DAL
{
    public class PicturyMarketDbContext : DbContext
    {
        public PicturyMarketDbContext(DbContextOptions<PicturyMarketDbContext> options) : base(options) {}
        public DbSet<Pictury> Picturies { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
    