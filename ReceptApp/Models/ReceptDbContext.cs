using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ReceptApp.Models
{
    public class ReceptDbContext : IdentityDbContext<User>
    {
        public ReceptDbContext(DbContextOptions<ReceptDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
