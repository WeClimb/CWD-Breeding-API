

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
            servicedocuments
        }

        public AzureBlobConfig(IConfiguration configuration, string userType, string fileCategory, string fileName, string fileExtension)
        {
            this._configuration = configuration;
            this.FullFileName = $"{fileName}.{fileExtension}";
            this.FileExtension = fileExtension;

            //if (this._configuration["DB_CONNECTION"].Contains(DBNames.UpperProdDB.GetDescriptionString()))
            //{
            //    switch (userType)
            //    {
            //        case UserLoginType.Customer:
            //            // Empty until blob storage is needed for customer portal
            //            // Add blob specific information here
            //            break;
            //        case UserLoginType.ServiceProvider:
            //            this.AccountName = "upperproviderportal";
            //            this.AccountKey = this._configuration["ProviderStorageAccountKey"];
            //            this.Container = fileCategory;
            //            break;
            //        case UserLoginType.Employee:
            //            // Empty until blob storage is needed for Employee portal
            //            // Add blob specific information here
            //            break;
            //    }
            //}
            //else
            //{
            //    switch (userType)
            //    {
            //        case UserLoginType.Customer:
            //            // Empty until blob storage is needed for customer portal
            //            // Add blob specific information here
            //            break;
            //        case UserLoginType.ServiceProvider:
            //            this.AccountName = "upperproviderportalqa";
            //            this.AccountKey = this._configuration["ProviderStorageAccountKey"];
            //            this.Container = fileCategory;
            //            break;
            //        case UserLoginType.Employee:
            //            // Empty until blob storage is needed for Employee portal
            //            // Add blob specific information here
            //            break;
            //    }
            //}

            // Create a URI to the blob  https://upperproviderportalqa.blob.core.windows.net/profile/upper5.png

            this.AccountName = "deerprofile";
            this.AccountKey = this._configuration["AzureAccountKey"];
            this.Container = "blobcontainer";

            this.BlobUri = new Uri("https://" +
                                  this.AccountName +
                                  ".blob.core.windows.net/" +
                                  this.Container +
                                  "/" + this.FullFileName);
        }
    }
}
