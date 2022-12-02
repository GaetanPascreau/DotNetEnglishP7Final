using Microsoft.EntityFrameworkCore;
using WebApi3.Domain;

namespace Dot.Net.WebApi.Data
{
    public class LocalDbContext : DbContext
    {
        public LocalDbContext(DbContextOptions<LocalDbContext> options)
           : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<BidList> BidList { get; set; }
        public DbSet<CurvePoint> CurvePoint { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<RuleName> RuleName { get; set; }
        public DbSet<Trade> Trade { get; set; }
    }
}