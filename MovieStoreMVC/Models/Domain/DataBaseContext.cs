using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MovieStoreMVC.Models.Domain
{
    public class DataBaseContext:IdentityDbContext<ApplicationUser>
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Ignore<SelectListGroup>();
            builder.Ignore <SelectListItem>();
            base.OnModelCreating(builder);
        }
        public DbSet<Movie> Movies { get; set;}
        public DbSet<Genre> Genres { get; set;}
        public DbSet<MovieGenre> MovieGenres { get; set; } 
    }
}
