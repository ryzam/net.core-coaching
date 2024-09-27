namespace _6_CustomAuthenticationAndAuthorization.Datas
{
    using _6_CustomAuthenticationAndAuthorization.Domains;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
