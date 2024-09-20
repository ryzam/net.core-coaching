using _2_TodoListWithLocalDb.Domains;
using Microsoft.EntityFrameworkCore;

namespace _2_TodoListWithLocalDb.Datas
{
    //Step 4
    public class CarAppsDbContext : DbContext
    {
        public CarAppsDbContext(DbContextOptions<CarAppsDbContext> options) : base(options) { }

        public DbSet<Car> Cars { get; set; }
    }
}
