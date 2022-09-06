namespace ReviewPlatformAPI.Models
{
    public class ReviewModel : BaseModel
    {
        public string Service { get; set; }
        public string Description { get; set; }
        public decimal Rating { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
