namespace ReviewPlatformAPI.Models
{
    public class ClientModel : BaseUserModel
    {
        public Guid SubDataId { get; set; }
        public SubDataModel? SubData { get; set; }
    }
}
