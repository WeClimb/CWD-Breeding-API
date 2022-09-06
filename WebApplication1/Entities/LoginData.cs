namespace ReviewPlatformAPI.Entities
{
    public class LoginData : BaseEntity
    {
        public string? Password { get; set; }
        public string? Salt { get; set; }
        public DateTime LastLoginDate { get; set; }
    }
}
