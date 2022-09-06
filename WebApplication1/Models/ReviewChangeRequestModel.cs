namespace ReviewPlatformAPI.Models
{
    public class ReviewChangeRequestModel : BaseModel
    {
        public Guid ReviewId { get; set; }
        public ReviewModel Review { get; set; }
        public Guid ChangeRequestId { get; set; }
        public ChangeRequestModel ChangeRequest { get; set; }
    }
}
