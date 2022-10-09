using CWDBreedingAPI.Constants;
using CWDBreedingAPI.Models.Non_EntityModels;
using CWDBreedingAPI.Utils;
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

        public DeerService(DeerRepo deerRepo, RanchService ranchService, AzureStorageHelper storageHelper, IConfiguration configuration)
        {
            _deerRepo = deerRepo;
            _ranchService = ranchService;
            _storageHelper = storageHelper;
            _configuration = configuration;
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
                Status = "ACTIVE"
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
            };
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
            
            exactAge = Math.Round(exactAge, 3);
            return exactAge;

        }

        public List<DeerModel> GetAll(bool isPending)
        {
            List<Deer> entityList = new List<Deer>();
            List<DeerModel> modelList = new List<DeerModel>();

            entityList = _deerRepo.GetAll(isPending);

            foreach (Deer deer in entityList)
            {
                DeerModel newModel = CreateModelForIndividualLookup(deer);
                newModel.deerFamily = MapDeerFamily(deer);
                modelList.Add(newModel);
            }

            return modelList;
        }

        public string CreateDeerRequest(DeerModel model)
        {
            Deer deer = ConverToEntityForAdd(model);
            deer.Id = Guid.NewGuid();
            model.Id = deer.Id;
            _deerRepo.Create(deer);
            _deerRepo.CreateDeerPedigree(model.Id,model.deerFamily);
            return deer.Id.ToString();
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

        public ProviderProfileImageModel GetProfileImageBytes(Guid deerId)
        {
            ProviderProfileImageModel model = new ProviderProfileImageModel();
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

        public bool SaveProfileImage(Guid deerId, IFormFile profileImg)
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

        public override BaseRepo<Deer> LoadRepo()
        {
            return _deerRepo;
        }
    }
}
