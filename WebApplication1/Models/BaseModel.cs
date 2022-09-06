namespace ReviewPlatformAPI.Models
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string? Status { get; set; }

    }
}
