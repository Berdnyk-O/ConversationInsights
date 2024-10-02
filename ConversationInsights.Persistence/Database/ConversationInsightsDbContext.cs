using ConversationInsights.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConversationInsights.Persistence.Database
{
    public sealed class ConversationInsightsDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Call> Calls { get; set; }
        public DbSet<CallCategory> CallCategories { get; set; }

        public ConversationInsightsDbContext(DbContextOptions<ConversationInsightsDbContext> opts)
            : base(opts) 
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Call>()
                .HasMany(e => e.Categories)
                .WithMany(e => e.Calls)
                .UsingEntity<CallCategory>();
        }
    }
}
