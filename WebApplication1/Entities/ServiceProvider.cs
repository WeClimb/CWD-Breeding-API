namespace ReviewPlatformAPI.Entities
{
    public class ServiceProvider : BaseUser
    {
        public Guid SubDataId { get; set; }
        public virtual SubData? SubData { get; set; }
    }
}
