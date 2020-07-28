using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ReceptApp.Models
{
    public class RecipeDbContext : IdentityDbContext<User>
    {
        public RecipeDbContext(DbContextOptions<RecipeDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<User> UserList { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Picture> Pictures { get; set; }
    }
}
