using ReviewPlatformAPI.Entities;
using ServiceProvider = ReviewPlatformAPI.Entities.ServiceProvider;

namespace ReviewPlatformAPI.Models
{
    public class ChangePasswordModel : BaseModel
    {
        public Guid? ClientId { get; set; }
        public Guid? ServiceProviderId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public virtual Client? Client { get; set; }
        public virtual ServiceProvider? ServiceProvider { get; set; }
    }
}
