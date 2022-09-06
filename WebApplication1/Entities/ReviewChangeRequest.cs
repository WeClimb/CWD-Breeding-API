namespace ReviewPlatformAPI.Entities
{
    public class ReviewChangeRequest : BaseEntity
    {
        public Guid ReviewId { get; set; }
        public virtual Review? Review { get; set; }
        public Guid ChangeRequestId { get; set; }
        public virtual ChangeRequest? ChangeRequest { get; set; }
    }
}
