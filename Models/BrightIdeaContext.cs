using Microsoft.EntityFrameworkCore;

namespace csharpbeltexam.Models
{
    public class BrightIdeaContext : DbContext
    {
        // INCLUDE ALL MODELS AS DBSETS: ie. public DbSet<User> Users { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BrightIdea> BrightIdeas { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public BrightIdeaContext(DbContextOptions<BrightIdeaContext> options) : base(options)
        { }
    }
}