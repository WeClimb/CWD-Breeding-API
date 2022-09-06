namespace ReviewPlatformAPI.Models
{
    public class LoginDataModel : BaseModel
    {
        public string Password { get; set; }
        public string Salt { get; set; }
        public DateTime LastLoginDate { get; set; }
    }
}
