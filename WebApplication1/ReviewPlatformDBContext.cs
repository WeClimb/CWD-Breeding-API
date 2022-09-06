using Microsoft.EntityFrameworkCore;
using ReviewPlatformAPI.Entities;
using ServiceProvider = ReviewPlatformAPI.Entities.ServiceProvider;

namespace ReviewPlatformAPI
{
    public class ReviewPlatformDBContext : DbContext
    {
        public ReviewPlatformDBContext(DbContextOptions<ReviewPlatformDBContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<SubData> SubData { get; set; }
        public DbSet<LoginData> LoginData { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ChangeRequest> ChangeRequests { get; set; }
        public DbSet<ReviewChangeRequest> ReviewsChangeRequests { get; set; }
        public DbSet<ClientReview> ClientsReviews { get; set; }
        public DbSet<ServiceProvider> ServiceProviders { get; set; }
        public DbSet<ServiceProviderReview> ServiceProvidersReviews { get; set; }
        public DbSet<ChangePassword> ChangePasswords { get; set; }
    }
}
