namespace ReviewPlatformAPI.Entities
{
    public abstract class BaseUser : BaseEntity
    {
        public String FirstName { get; set; } = string.Empty;
        public String LastName { get; set; } = string.Empty;
        public String Email { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public Guid LoginDataId { get; set; }
        public virtual LoginData? LoginData { get; set; }
       
    }
}
