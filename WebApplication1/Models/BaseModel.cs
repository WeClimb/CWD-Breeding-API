namespace ReviewPlatformAPI.Models
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        public string? Status { get; set; } = null!;

    }
}
