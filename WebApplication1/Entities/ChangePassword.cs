using ServiceProvider = ReviewPlatformAPI.Entities.ServiceProvider;

namespace ReviewPlatformAPI.Entities

{
    public class ChangePassword : BaseEntity
    {
        public Guid? ClientId { get; set; }
        public Guid? ServiceProviderId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public virtual Client? Client { get; set; }
        public virtual ServiceProvider? ServiceProvider { get; set; }
    }
}
