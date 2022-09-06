namespace ReviewPlatformAPI.Models
{
    public abstract class BaseUserModel : BaseModel
    {
        //TODO: REMOVE LOGIN DATA FOR FE MODEL
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public Guid LoginDataId { get; set; }
        public LoginDataModel? LoginData { get; set; }
    }
}
