using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;
using CWDBreedingAPI.Constants;

namespace Upper_Backend.Utils
{
    public class AzureStorageHelper
    {
        private readonly IConfiguration _configuration;

        public AzureStorageHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public byte[] DownloadFile(string url)
        {
            WebClient Client = new WebClient();
            byte[] imgByte = Client.DownloadData($"{url}");

            return imgByte;
        }

        public string ConvertToBase64Format(string base64String, string contentType)
        {
            return $"data:{contentType};base64,{base64String}";
        }

        public bool DoesStorageFileExist(AzureBlobConfig storageConfig)
        {
            bool blobExists = false;
            BlobContainerClient blobContainer = new BlobContainerClient(storageConfig.AccountKey, storageConfig.Container);

            var blobs = blobContainer.GetBlobs(prefix: storageConfig.FullFileName.Substring(0, storageConfig.FullFileName.LastIndexOf('.')));

            // blobs should always return 1 result
            // however since GetBlobs returns an enumerator there is no easy way to see if anything was populated
            // so this is done via this foreach, even though it is not the most efficient operation
            foreach (var blob in blobs)
            {
                blobExists = true;
            }

            return blobExists;
        }

        public bool UploadFileToStorage(Stream fileStream, AzureBlobConfig storageConfig)
        {
            BlobContainerClient blobContainer = new BlobContainerClient(storageConfig.AccountKey, storageConfig.Container);

            BinaryData data = BinaryData.FromStream(fileStream);

            fileStream.Close();

            var didUpload = blobContainer.UploadBlob(storageConfig.FullFileName, data);

            return didUpload.Value != null;
        }

        public bool RemoveFileFromStorage(AzureBlobConfig storageConfig)
        {
            BlobContainerClient blobContainer = new BlobContainerClient(storageConfig.AccountKey, storageConfig.Container);

            var blobs = blobContainer.GetBlobs(prefix: storageConfig.FullFileName.Substring(0, storageConfig.FullFileName.LastIndexOf('.')));

            // blobs should always return 1 result
            // however since GetBlobs returns an enumerator there is no easy way to see if anything was populated
            // so this is done via this foreach, even though it is not the most efficient operation
            foreach (var blob in blobs)
            {
                blobContainer.DeleteBlobIfExists(blob.Name);
            }

            return true;
        }
    }
}
