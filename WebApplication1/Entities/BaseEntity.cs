namespace ReviewPlatformAPI.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate{ get; set; }
        public string? Status { get; set; }
    }
}
