using Microsoft.EntityFrameworkCore;

namespace HotTop
{
    public class HotDbContext: DbContext
    {
        public HotDbContext(DbContextOptions<HotDbContext> options): base(options)
        {

        }

        public DbSet<HotTop> HotTops {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=hottop.db");
        }
    }
}