namespace ReviewPlatformAPI.Models
{
    public class ClientReviewModel : BaseModel
    {
        public Guid ReviewId { get; set; }
        public ReviewModel Review { get; set; }
        public Guid ClientId { get; set; }
        public ClientModel Client { get; set; }
    }
}
