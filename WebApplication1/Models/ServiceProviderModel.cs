namespace ReviewPlatformAPI.Models
{
    public class ServiceProviderModel : BaseUserModel
    {
        public Guid SubDataId { get; set; }
        public SubDataModel? SubData { get; set; }
    }
}
