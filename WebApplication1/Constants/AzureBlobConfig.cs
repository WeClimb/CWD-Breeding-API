

namespace CWDBreedingAPI.Constants
{
    public class AzureBlobConfig
    {
        private readonly IConfiguration _configuration;
        public string AccountName { get; }
        public string Container { get; }
        public string AccountKey { get; }
        public string FullFileName { get; }
        public string FileExtension { get; }
        public string FileContentType { get; }
        public Uri BlobUri { get; }
        public enum FileCategories
        {
            profile,
            image,
            video
        }

        public AzureBlobConfig(IConfiguration configuration, string userType, string fileCategory, string fileName, string fileExtension)
        {
            _configuration = configuration;
            FullFileName = $"{fileName}.{fileExtension}";
            FileExtension = fileExtension;

            // Create a URI to the blob  https://upperproviderportalqa.blob.core.windows.net/profile/upper5.png
            //                                     https://deerprofile.blob.core.windows.net/blobcontainer

            AccountName = "deerprofile";
            AccountKey = _configuration["AzureAccountKey"];
            Container = "blobcontainer";

            BlobUri = new Uri("https://" +
                                  this.AccountName +
                                  ".blob.core.windows.net/" +
                                  this.Container +
                                  "/" + this.FullFileName);
        }
    }
}
