namespace ReviewPlatformAPI.Entities
{
    public class ClientReview : BaseEntity
    {
        public Guid ReviewId { get; set; }
        public virtual Review? Review { get; set; }
        public Guid ClientId { get; set; }
        public virtual Client? Client { get; set; }
    }
}
