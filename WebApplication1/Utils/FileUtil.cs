using FileTypeChecker;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;

namespace CWDBreedingAPI.Utils
{
    public static class FileUtil
    {
        public static readonly List<string> _validTypes = new List<string>()
        {
            FileTypeChecker.Types.PortableDocumentFormat.TypeName,
            FileTypeChecker.Types.MicrosoftOfficeDocument.TypeName,
            FileTypeChecker.Types.MicrosoftOffice365Document.TypeName,
            FileTypeChecker.Types.PortableNetworkGraphic.TypeName,
            FileTypeChecker.Types.JointPhotographicExpertsGroup.TypeName,
        };

        public static readonly int _fileSizeBytes = 10000000; // 10MB

        public static bool IsValidFileType(IFormFile file)
        {
            bool isValid = false;

            using (var fileStream = file.OpenReadStream())
            {
                var fileType = FileTypeValidator.GetFileType(fileStream);

                if (_validTypes.Contains(fileType.Name))
                {
                    isValid = true;
                }

                fileStream.Close();
            }

            return isValid;
        }

        public static bool IsValidFileType(Stream fileStream)
        {
            bool isValid = false;

            var fileType = FileTypeValidator.GetFileType(fileStream);

            if (_validTypes.Contains(fileType.Name))
            {
                isValid = true;
            }

            fileStream.Position = 0;

            return isValid;
        }

        public static bool IsFileCorrectSize(IFormFile file)
        {
            bool isValid = false;

            if(file.Length <= _fileSizeBytes)
            {
                isValid = true;
            }

            return isValid;
        }

        public static string GetFileType(IFormFile file)
        {
            string fileType;

            using(var fileStream = file.OpenReadStream())
            {
                fileType = FileTypeValidator.GetFileType(fileStream).Extension;
            }

            return fileType;
        }

        public static string GetFileType(Stream fileStream)
        {
            string fileType;

            fileType = FileTypeValidator.GetFileType(fileStream).Extension;

            fileStream.Position = 0;

            return fileType;
        }

        public static string GetContentType(byte[] fileData)
        {
            string fileType = GetFileType(new MemoryStream(fileData));

            return $"image/{fileType}";
        }
    }
}
