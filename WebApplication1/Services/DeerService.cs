using CWDBreedingAPI.Constants;
using CWDBreedingAPI.Models.Non_EntityModels;
using CWDBreedingAPI.Utils;
using ReviewPlatformAPI.Constants;
using ReviewPlatformAPI.Entities;
using ReviewPlatformAPI.Models;
using ReviewPlatformAPI.Repos;
using static CWDBreedingAPI.Constants.AzureBlobConfig;

namespace ReviewPlatformAPI.Services
{
    public class DeerService : BaseService<DeerModel, Deer>
    {
        private readonly DeerRepo _deerRepo;
        private readonly RanchService _ranchService;
        private readonly AzureStorageHelper _storageHelper;
        private readonly IConfiguration _configuration;
        private readonly EmailService _emailService;

        public DeerService(DeerRepo deerRepo, RanchService ranchService, AzureStorageHelper storageHelper, IConfiguration configuration, EmailService emailService)
        {
            _deerRepo = deerRepo;
            _ranchService = ranchService;
            _storageHelper = storageHelper;
            _configuration = configuration;
            _emailService = emailService;
        }
        public override Deer ConverToEntityForAdd(DeerModel model)
        {
            return new Deer
            {
                Name = model.Name,
                Nadr = model.Nadr,
                Dob = model.Dob,
                Age = CalculateAgeFromDOB(model.Dob),
                Gebu = model.Gebu,
                Codon = model.Codon,
                SciScore = model.SciScore,
                IsApproved = false,
                SemenAvailable = model.SemenAvailable,
                SemenCost = model.SemenCost,
                RanchId = model.RanchId,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                Status = "ACTIVE",
                VideoLink = model.VideoLink,
                ProfileImage = model.ProfileImage,
                DenialReason = model.DenialReason,
                AgeOfBuckDisplayed = model.AgeOfBuckDisplayed,
                Description = model.Description,
                IsPaid = model.IsPaid,
                PaidDate = model.PaidDate,
            };
        }

        public DeerModel? GetById(Guid id)
        {
            Deer? deer = _deerRepo.GetById(id);
            if (deer != null)
            {
                DeerModel newModel = CreateModelForIndividualLookup(deer);
                newModel.deerFamily = MapDeerFamily(deer);
                return newModel;
            }
            else
            {
                return null;
            }
        }
        public override void CopyDataForUpdate(Deer entity, DeerModel model)
        {
            entity.Status = model.Status ?? "ACTIVE";
            entity.CreateDate = model.CreateDate;
            entity.UpdateDate = DateTime.Now;
            entity.Age = CalculateAgeFromDOB(model.Dob);
            entity.SemenCost = model.SemenCost;
            entity.IsApproved = model.IsApproved;
            entity.SemenAvailable = model.SemenAvailable;
            entity.RanchId = model.RanchId;
            entity.Nadr = model.Nadr;
            entity.Codon = model.Codon;
            entity.Name = model.Name;
            entity.Dob = model.Dob;
            entity.SciScore = model.SciScore;
            entity.Gebu = model.Gebu;
            entity.ProfileImage = model.ProfileImage;
            entity.DenialReason = model.DenialReason;
            entity.VideoLink = model.VideoLink;
            entity.AgeOfBuckDisplayed = model.AgeOfBuckDisplayed;
            entity.Description = model.Description;
            entity.IsPaid = model.IsPaid;
            
            if(model.Status.ToLower() == "denied")
            {
                entity.Status = model.Status.ToUpper();
            }

            if(model.Status.ToLower() != "denied")
            {
                entity.Status = "ACTIVE";
            }

            if(entity.PaidDate != null)
            {
                entity.PaidDate = model.PaidDate;
            }
        }

        public override DeerModel CreateModelForIndividualLookup(Deer entity)
        {
            return new DeerModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Nadr = entity.Nadr,
                Dob = entity.Dob,
                Age = entity.Age,
                Gebu = entity.Gebu,
                Codon = entity.Codon,
                SciScore = entity.SciScore,
                IsApproved = entity.IsApproved,
                SemenAvailable = entity.SemenAvailable,
                SemenCost = entity.SemenCost,
                RanchId = entity.RanchId,
                Ranch = _ranchService.CreateModelForIndividualLookup(entity.Ranch),
                VideoLink = entity.VideoLink,
                ProfileImage = entity.ProfileImage,
                DenialReason = entity.DenialReason,
                AgeOfBuckDisplayed = entity.AgeOfBuckDisplayed,
                Description = entity.Description,
                IsPaid = entity.IsPaid,
                PaidDate = entity.PaidDate,
                Status = entity.Status,
            };
        }

        public bool UpdateDeerFamily(Guid deerId, DeerFamilyModel deerFamily)
        {
            try
            {
                Deer? deer = _deerRepo.GetById(deerId);

                if (deer != null)
                {
                    LevelOneRelationship levelOne = deer.LevelOneRelationships.First();
                    LevelTwoRelationship levelTwo = deer.LevelTwoRelationships.First();
                    LevelThreeRelationship levelThree = deer.LevelThreeRelationships.First();

                    // Update LevelOne relationships
                    levelOne.Sire = !string.IsNullOrEmpty(deerFamily.LevelOneSire) ? deerFamily.LevelOneSire : levelOne.Sire;
                    levelOne.Dam = !string.IsNullOrEmpty(deerFamily.LevelOneDam) ? deerFamily.LevelOneDam : levelOne.Dam;

                    // Update LevelTwo relationships
                    levelTwo.SireA = !string.IsNullOrEmpty(deerFamily.LevelTwoSireA) ? deerFamily.LevelTwoSireA : levelTwo.SireA;
                    levelTwo.SireB = !string.IsNullOrEmpty(deerFamily.LevelTwoSireB) ? deerFamily.LevelTwoSireB : levelTwo.SireB;
                    levelTwo.DamA = !string.IsNullOrEmpty(deerFamily.LevelTwoDamA) ? deerFamily.LevelTwoDamA : levelTwo.DamA;
                    levelTwo.DamB = !string.IsNullOrEmpty(deerFamily.LevelTwoDamB) ? deerFamily.LevelTwoDamB : levelTwo.DamB;

                    // Update LevelThree relationships
                    levelThree.SireA = !string.IsNullOrEmpty(deerFamily.LevelThreeSireA) ? deerFamily.LevelThreeSireA : levelThree.SireA;
                    levelThree.SireB = !string.IsNullOrEmpty(deerFamily.LevelThreeSireB) ? deerFamily.LevelThreeSireB : levelThree.SireB;
                    levelThree.SireC = !string.IsNullOrEmpty(deerFamily.LevelThreeSireC) ? deerFamily.LevelThreeSireC : levelThree.SireC;
                    levelThree.SireD = !string.IsNullOrEmpty(deerFamily.LevelThreeSireD) ? deerFamily.LevelThreeSireD : levelThree.SireD;
                    levelThree.DamA = !string.IsNullOrEmpty(deerFamily.LevelThreeDamA) ? deerFamily.LevelThreeDamA : levelThree.DamA;
                    levelThree.DamB = !string.IsNullOrEmpty(deerFamily.LevelThreeDamB) ? deerFamily.LevelThreeDamB : levelThree.DamB;
                    levelThree.DamC = !string.IsNullOrEmpty(deerFamily.LevelThreeDamC) ? deerFamily.LevelThreeDamC : levelThree.DamC;
                    levelThree.DamD = !string.IsNullOrEmpty(deerFamily.LevelThreeDamD) ? deerFamily.LevelThreeDamD : levelThree.DamD;

                    _deerRepo.Update(deer);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public bool UpdateImageAge(Guid deerId, string url,int newAge, bool isProfileImage)
        {
            return _deerRepo.UpdateImageAge(deerId, url, newAge, isProfileImage);
        }

        public bool RemoveImage(Guid deerId, string imageUrl)
        {
            return _deerRepo.RemoveImage(deerId, imageUrl);
        }

        public bool SwapProfileImage(Guid deerId, string profileImageUrl, string imageUrl)
        {
            return _deerRepo.SwapProfileImage(deerId, profileImageUrl, imageUrl);
        }

        private ICollection<MediaModel> MapMedia(ICollection<Media> media)
        {
            List<MediaModel> mediaModels = new List<MediaModel>();
            foreach (Media item in media)
            {
                MediaModel newMedia = new MediaModel();
                newMedia.Id = item.Id;
                newMedia.DeerId = item.DeerId;
                newMedia.UpdateDate = item.UpdateDate;
                newMedia.CreateDate = item.CreateDate;
                newMedia.Status = item.Status;
                newMedia.BlobId = item.BlobId;
                newMedia.Type = item.Type;

                mediaModels.Add(newMedia);
            }
            
            return mediaModels;
        }
        public override DeerModel CreateModelForListLookup(Deer entity)
        {
            return new DeerModel
            {
                Status = entity.Status,
                CreateDate = entity.CreateDate,
                UpdateDate = entity.UpdateDate,
                Id = entity.Id,
            };
        }

        private decimal? CalculateAgeFromDOB(DateTime birthDate)
        {
            // get current date (don't call DateTime.Today repeatedly, as it changes)
            DateTime today = DateTime.Today;
            // get the last birthday
            int years = today.Year - birthDate.Year;
            DateTime last = birthDate.AddYears(years);
            if (last > today)
            {
                last = last.AddYears(-1);
                years--;
            }
            // get the next birthday
            DateTime next = last.AddYears(1);
            // calculate the number of days between them
            decimal yearDays = (next - last).Days;
            // calcluate the number of days since last birthday
            decimal days = (today - last).Days;
            // calculate exaxt age
            decimal exactAge = (decimal)years + (days / yearDays);

            exactAge = Math.Round(exactAge, 4);
            return exactAge;

        }

        public List<DeerModel> GetAll(bool isPending, bool isPaid)
        {
            List<Deer> entityList = new List<Deer>();
            List<DeerModel> modelList = new List<DeerModel>();

            entityList = _deerRepo.GetAll(isPending, isPaid);

            foreach (Deer deer in entityList)
            {
                DeerModel newModel = CreateModelForIndividualLookup(deer);
                newModel.deerFamily = MapDeerFamily(deer);
                modelList.Add(newModel);
            }

            return modelList;
        }

        public List<DeerModel> GetAllFiltered(bool isApproved, string? deerName, string? ranchName, string? codon, decimal? gebv, int? age, int? sciScore, int? page, string? ranchId)
        {
            List<Deer> entityList = new List<Deer>();
            List<DeerModel> modelList = new List<DeerModel>();
            
            if (deerName == null)
            {
                deerName = "";
            }

            if (ranchName == null)
            {
                ranchName = "";
            }

            if (codon == null)
            {
                codon = "";
            }

            if (ranchId == null)
            {
                ranchId = "";
            }

            entityList = _deerRepo.GetAllFiltered(isApproved, deerName, ranchName, codon, gebv, age, sciScore, page, ranchId);

            foreach (Deer deer in entityList)
            {
                DeerModel newModel = CreateModelForIndividualLookup(deer);
                newModel.deerFamily = MapDeerFamily(deer);
                modelList.Add(newModel);
            }

            return modelList;
        }

        public void DenyRequest(DeerModel deer)
        {
            Ranch? ranch = _deerRepo.GetRanch(deer.RanchId);
            deer.Status = "DENIED";
            Update(deer.Id, deer);

            string fullName = deer.Ranch.OwnerFirstName + " " + deer.Ranch.OwnerlastName;
            string loginLink = _configuration["CurrentHost"] + "login";

            if (ranch != null)
            {
                bool emailStatus = _emailService.SendEmail(
                                   ranch.Email,
                                   EmailConstants.DenyDeerSubject,
                                   string.Format(EmailConstants.DenyDeerBody, fullName, deer.Name, deer.DenialReason, loginLink),
                                   null
                );
            }
        }
        public void ApproveRequest(DeerModel deer)
        {
            Ranch? ranch = _deerRepo.GetRanch(deer.RanchId);
            deer.Status = "ACTIVE";
            deer.DenialReason = null;
            Update(deer.Id, deer);

            string loginLink = _configuration["CurrentHost"] + "login";

            if (ranch != null)
            {
                bool emailStatus = _emailService.SendEmail(
                                   ranch.Email,
                                   EmailConstants.ApproveDeerRequestSubject,
                                   string.Format(EmailConstants.ApproveDeerRequestBody, deer.Name, loginLink),
                                   null
                );
            }
        }
        public string CreateDeerRequest(DeerModel model)
        {
            Deer deer = ConverToEntityForAdd(model);
            deer.Id = Guid.NewGuid();
            model.Id = deer.Id;
            _deerRepo.Create(deer);
            _deerRepo.CreateDeerPedigree(model.Id, model.deerFamily);

            //Ranch? ranch = _deerRepo.GetRanch(deer.RanchId);        

            //string fullName = deer.Ranch.OwnerFirstName + " " + deer.Ranch.OwnerlastName;
            //string loginLink = _configuration["CurrentHost"] + "login";

            //if (ranch != null)
            //{
            //    bool emailStatus = _emailService.SendEmail(
            //                       ranch.Email,
            //                       EmailConstants.DeerSubmissionSubject,
            //                       string.Format(EmailConstants.DeerSubmissionBody, fullName, deer.Name, loginLink),
            //                       null
            //    );
            //}
            return deer.Id.ToString();
        }

        public void SendAddDeerEmail(DeerModel[] deerList)
        {
            Ranch? ranch = _deerRepo.GetRanch(deerList[0].RanchId);
            string fullName = ranch!.OwnerFirstName + " " + ranch!.OwnerlastName;
            string loginLink = _configuration["CurrentHost"] + "login";

            string deerCSV = "";

            foreach (DeerModel deer in deerList)
            {
                deerCSV = deerCSV + deer.Name + ",";
            }
            
            if (ranch != null)
            {
                bool emailStatus = _emailService.SendEmail(
                                   ranch.Email,
                                   EmailConstants.DeerSubmissionSubject,
                                   string.Format(EmailConstants.DeerSubmissionBody, fullName, deerCSV, loginLink),
                                   null
                );
            }
        }

        public DeerFamilyModel MapDeerFamily(Deer deer)
        {
            DeerFamilyModel family = new DeerFamilyModel();

            family.LevelOneDam = deer.LevelOneRelationships.OfType<LevelOneRelationship>().FirstOrDefault().Dam ?? "unkown";
            family.LevelOneSire = deer.LevelOneRelationships.OfType<LevelOneRelationship>().FirstOrDefault().Sire ?? "unkown";

            family.LevelTwoDamA = deer.LevelTwoRelationships.OfType<LevelTwoRelationship>().FirstOrDefault().DamA ?? "unkown";
            family.LevelTwoDamB = deer.LevelTwoRelationships.OfType<LevelTwoRelationship>().FirstOrDefault().DamB ?? "unkown";
            family.LevelTwoSireA = deer.LevelTwoRelationships.OfType<LevelTwoRelationship>().FirstOrDefault().SireA ?? "unkown";
            family.LevelTwoSireB = deer.LevelTwoRelationships.OfType<LevelTwoRelationship>().FirstOrDefault().SireB ?? "unkown";

            family.LevelThreeDamA = deer.LevelThreeRelationships.OfType<LevelThreeRelationship>().FirstOrDefault().DamA ?? "unkown";
            family.LevelThreeDamB = deer.LevelThreeRelationships.OfType<LevelThreeRelationship>().FirstOrDefault().DamB ?? "unkown";
            family.LevelThreeDamC = deer.LevelThreeRelationships.OfType<LevelThreeRelationship>().FirstOrDefault().DamC ?? "unkown";
            family.LevelThreeDamD = deer.LevelThreeRelationships.OfType<LevelThreeRelationship>().FirstOrDefault().DamD ?? "unkown";
            family.LevelThreeSireA = deer.LevelThreeRelationships.OfType<LevelThreeRelationship>().FirstOrDefault().SireA ?? "unkown";
            family.LevelThreeSireB = deer.LevelThreeRelationships.OfType<LevelThreeRelationship>().FirstOrDefault().SireB ?? "unkown";
            family.LevelThreeSireC = deer.LevelThreeRelationships.OfType<LevelThreeRelationship>().FirstOrDefault().SireC ?? "unkown";
            family.LevelThreeSireD = deer.LevelThreeRelationships.OfType<LevelThreeRelationship>().FirstOrDefault().SireD ?? "unkown";

            return family;
        }

        public DeerProfileImageModel GetProfileImageBytes(Guid deerId)
        {
            DeerProfileImageModel model = new DeerProfileImageModel();
            string profileImageUrl;
            DeerModel deer = GetById(deerId);

            if (deer != null)
            {
                if (String.IsNullOrEmpty(deer.ProfileImage))
                {
                    profileImageUrl = "unknown";
                }
                else
                {
                    profileImageUrl = deer.ProfileImage;
                }

                byte[] fileData = _storageHelper.DownloadFile(profileImageUrl);

                model.ContentType = FileUtil.GetContentType(fileData);
                model.ImageData = _storageHelper.ConvertToBase64Format(Convert.ToBase64String(fileData), model.ContentType);

                return model;
            }
            else
            {
                throw new Exception("Deer does not exist");
            }
        }

        public bool SaveProfileImage(Guid deerId, IFormFile profileImg, int? age)
        {
            DeerModel deer = GetById(deerId);

            if (deer == null)
            {
                throw new Exception("Not a valid Deer");
            }

            // Create Stream
            Stream dataStream = profileImg.OpenReadStream();

            // Create the config for accessing the blob storage
            AzureBlobConfig azureConfig = new AzureBlobConfig(_configuration, "Ranch", FileCategories.profile.ToString(), deer.Id.ToString(), FileUtil.GetFileType(dataStream));

            // Image must be a PNG or JPG
            if (azureConfig.FileExtension.ToLower().Equals(FileTypeChecker.Types.PortableNetworkGraphic.TypeExtension.ToLower()) || azureConfig.FileExtension.ToLower().Equals(FileTypeChecker.Types.JointPhotographicExpertsGroup.TypeExtension.ToLower()))
            {
                // Check if there is already a profile image related to this user, if so delete on Azure
                if (_storageHelper.DoesStorageFileExist(azureConfig))
                {
                    _storageHelper.RemoveFileFromStorage(azureConfig);
                }

                // Verify file size
                if (!FileUtil.IsFileCorrectSize(profileImg))
                {
                    throw new Exception($"File size too large, must be {FileUtil._fileSizeBytes / 1000} or smaller");
                }

                // Upload file
                if (_storageHelper.UploadFileToStorage(dataStream, azureConfig))
                {
                    deer.ProfileImage = azureConfig.BlobUri.ToString();
                    deer.AgeOfBuckDisplayed = age;

                    Update(deer.Id, deer);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new Exception($"File type of {azureConfig.FileExtension} is not a valid file type");
            }
        }


        public bool SaveImage(Guid deerId, IFormFile profileImg, int? age)
        {
            DeerModel deer = GetById(deerId);

            if (deer == null)
            {
                throw new Exception("Not a valid Deer");
            }

            // Create Stream
            Stream dataStream = profileImg.OpenReadStream();

            // Create the config for accessing the blob storage
            AzureBlobConfig azureConfig = new AzureBlobConfig(_configuration, "Ranch", FileCategories.image.ToString(), Guid.NewGuid().ToString(), FileUtil.GetFileType(dataStream));

            // Image must be a PNG or JPG
            if (azureConfig.FileExtension.ToLower().Equals(FileTypeChecker.Types.PortableNetworkGraphic.TypeExtension.ToLower()) || azureConfig.FileExtension.ToLower().Equals(FileTypeChecker.Types.JointPhotographicExpertsGroup.TypeExtension.ToLower()))
            {
                // Check if there is already a profile image related to this user, if so delete on Azure
                if (_storageHelper.DoesStorageFileExist(azureConfig))
                {
                    _storageHelper.RemoveFileFromStorage(azureConfig);
                }

                // Verify file size
                if (!FileUtil.IsFileCorrectSize(profileImg))
                {
                    throw new Exception($"File size too large, must be {FileUtil._fileSizeBytes / 1000} or smaller");
                }

                // Upload file
                if (_storageHelper.UploadFileToStorage(dataStream, azureConfig))
                {
                    string imageUrl = azureConfig.BlobUri.ToString();

                    AddImage(deer.Id, imageUrl, age);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new Exception($"File type of {azureConfig.FileExtension} is not a valid file type");
            }
        }

        public Dictionary<string, int?> GetImageBytes(Guid deerId)
        {
            Dictionary<string, int?> imageBytes = new Dictionary<string, int?>();
            ICollection<Media> deerMedia = _deerRepo.GetDeerMedia(deerId);
            if (deerMedia.Count != 0)
            {
                foreach (var image in deerMedia)
                {
                    int? ageOfBuckDisplayed = image.AgeOfBuckDisplayed;
                    if (!ageOfBuckDisplayed.HasValue)
                    {
                        ageOfBuckDisplayed = 0;
                    }
                    imageBytes.Add(image.BlobId, ageOfBuckDisplayed);
                }

                return imageBytes;
            }
            else
            {
                return imageBytes;
            }
        }


        private void AddImage(Guid deerId, string imageUrl, int? age)
        {
            _deerRepo.SaveImageToDeer(deerId, imageUrl, age);
        }

        public override BaseRepo<Deer> LoadRepo()
        {
            return _deerRepo;
        }
    }
}
