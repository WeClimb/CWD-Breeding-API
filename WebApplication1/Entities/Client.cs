namespace ReviewPlatformAPI.Entities
{
    public class Client : BaseUser
    {
        public Guid SubDataId { get; set; }
        public virtual SubData? SubData { get; set; }
    }
}
