using CWDBreedingAPI.Models.Non_EntityModels;
using Microsoft.EntityFrameworkCore;
using ReviewPlatformAPI.Entities;
using ReviewPlatformAPI.Models.Non_EntityModels;
using System.Linq.Dynamic.Core;

namespace ReviewPlatformAPI.Repos
{
    public class DeerRepo : BaseRepo<Deer>
    {
        public DeerRepo(CWDBreedingContext reviewPlatformDBContext) : base(reviewPlatformDBContext)
        {
        }

        public bool CreateDeerPedigree(Guid deerId, DeerFamilyModel family)
        {
            try
            {
                LevelOneRelationship levelOne = new LevelOneRelationship();
                levelOne.Id = Guid.NewGuid();
                levelOne.DeerId = deerId;
                levelOne.Status = "ACTIVE";
                levelOne.CreateDate = DateTime.Now;
                levelOne.UpdateDate = DateTime.Now;
                levelOne.Dam = family.LevelOneDam;
                levelOne.Sire = family.LevelOneSire;

                LevelTwoRelationship levelTwo = new LevelTwoRelationship();
                levelTwo.Id = Guid.NewGuid();
                levelTwo.DeerId = deerId;
                levelTwo.Status = "ACTIVE";
                levelTwo.CreateDate = DateTime.Now;
                levelTwo.UpdateDate = DateTime.Now;
                levelTwo.DamA = family.LevelTwoDamA;
                levelTwo.SireA = family.LevelTwoSireA;
                levelTwo.DamB = family.LevelTwoDamB;
                levelTwo.SireB = family.LevelTwoSireB;

                LevelThreeRelationship levelThree = new LevelThreeRelationship();
                levelThree.Id = Guid.NewGuid();
                levelThree.DeerId = deerId;
                levelThree.Status = "ACTIVE";
                levelThree.CreateDate = DateTime.Now;
                levelThree.UpdateDate = DateTime.Now;
                levelThree.SireA = family.LevelThreeSireA;
                levelThree.DamA = family.LevelThreeDamA;
                levelThree.SireB = family.LevelThreeSireB;
                levelThree.DamB = family.LevelThreeDamB;
                levelThree.SireC = family.LevelThreeSireC;
                levelThree.DamC = family.LevelThreeDamC;
                levelThree.SireD = family.LevelThreeSireD;
                levelThree.DamD = family.LevelThreeDamD;

                _reviewPlatformDBContext.LevelOneRelationships.Add(levelOne);
                _reviewPlatformDBContext.LevelTwoRelationships.Add(levelTwo);
                _reviewPlatformDBContext.LevelThreeRelationships.Add(levelThree);

                if(_reviewPlatformDBContext.SaveChanges() == 3){
                    return true;
                } 
                else
                {
                    return false;
                }
            }
            catch
            {
                throw new Exception("Family Creation Failed");
            }
       
        }

        public ICollection<Media> GetDeerMedia(Guid deerId)
        {
            return _reviewPlatformDBContext.Media.Where(media => media.DeerId == deerId).ToList();
        }
        public List<Deer> GetAllFiltered(bool isApproved, string? deerName, string? ranchName, string? codon, decimal? gebv, int? age, int? sciScore, int? page, string? ranchId)
        {
            int pageSize = 10; // Set the page size to 10, can be changed to any value

            if (!string.IsNullOrEmpty(ranchId))
            {
                return LoadDbSet()
                        .Where(deer => deer.RanchId.ToString() == ranchId)
                        .Include(deer => deer.Ranch)
                        .Include(deer => deer.LevelOneRelationships)
                        .Include(deer => deer.LevelTwoRelationships)
                        .Include(deer => deer.LevelThreeRelationships)
                        .ToList();
            }

            IQueryable<Deer> query = LoadDbSet().Where(deer => deer.IsApproved == isApproved)
                                          .Where(deer => deer.IsPaid == true)
                                          .Where(deer => string.IsNullOrEmpty(deerName) || deer.Name.Contains(deerName))
                                          .Where(deer => string.IsNullOrEmpty(ranchName) || deer.Ranch.Name.Contains(ranchName))
                                          .Where(deer => string.IsNullOrEmpty(codon) || deer.Codon.Contains(codon))
                                          .Where(deer => deer.Status.ToLower() != "denied");

            if (gebv != null)
            {
                query = query.Where(deer => deer.GEBV <= gebv);
            }

            if (age != null)
            {
                query = query.Where(deer => deer.Age == age);
            }

            if (sciScore != null)
            {
                query = query.Where(deer => deer.SciScore >= sciScore);
            }

            if (!string.IsNullOrEmpty(ranchId))
            {
                query = query.Where(deer => deer.RanchId.ToString().Contains(ranchId));
            }

            if (page != null && page > 0)
            {
                query = query.Skip(pageSize * ((int)page - 1)).Take(pageSize);
            }

            var result = query.Include(deer => deer.Ranch)
                              .Include(deer => deer.LevelOneRelationships)
                              .Include(deer => deer.LevelTwoRelationships)
                              .Include(deer => deer.LevelThreeRelationships)
                              .ToList();

            return result;
        }


        public void SaveImageToDeer(Guid deerId, string imageUrl, int? age)
        {
            Media media = new Media();
            media.Id = Guid.NewGuid();
            media.DeerId = deerId;
            media.CreateDate = DateTime.Now;
            media.UpdateDate = DateTime.Now;
            media.Status = "ACTIVE";
            media.BlobId = imageUrl;
            media.Type = "image";
            media.AgeOfBuckDisplayed = age;

            _reviewPlatformDBContext.Media.Add(media);
            _reviewPlatformDBContext.SaveChanges();
        }

        public Ranch? GetRanch(Guid ranchId)
        {
            Ranch? ranch = _reviewPlatformDBContext.Ranches.Find(ranchId);
            return ranch;
        }

        public Deer? GetById(Guid id)
        {
            return LoadDbSet().Where(deer => deer.Id == id)
                              .Include(deer => deer.Ranch)
                              .Include(deer => deer.LevelOneRelationships)
                              .Include(deer => deer.LevelTwoRelationships)
                              .Include(deer => deer.LevelThreeRelationships)
                              .FirstOrDefault();
        }

        public List<Deer> GetAll(bool isPending, bool isPaid)
        {
            return LoadDbSet().Where(deer => deer.IsApproved == isPending && deer.IsPaid == isPaid)
                              .Where(deer => deer.Status.ToLower() != "denied")
                              .Include(deer => deer.LevelOneRelationships)
                              .Include(deer => deer.LevelTwoRelationships)
                              .Include(deer => deer.LevelThreeRelationships)
                              .Include(deer => deer.Ranch)
                              .ToList();
        }

        public override DbSet<Deer> LoadDbSet()
        {
            return _reviewPlatformDBContext.Deer;
        }

        public bool SwapProfileImage(Guid deerId, string profileImageUrl, string imageUrl)
        {
            Deer? deer = LoadDbSet().Where(deer => deer.Id == deerId && deer.ProfileImage.ToLower() == profileImageUrl.ToLower())
                                    .Include(deer => deer.Media)
                                    .FirstOrDefault();

            if(deer != null)
            {
                deer.ProfileImage = imageUrl;

                var media = deer.Media.Where(media => media.BlobId.ToLower() == imageUrl.ToLower()).FirstOrDefault();
                if(media != null)
                {
                    media.BlobId = profileImageUrl;

                    return _reviewPlatformDBContext.SaveChanges() > 0;
                }
                else
                {
                    return false;
                }
            } 
            else
            {
                return false;
            }
        }

        public bool UpdateImageAge(Guid deerId, string url, int newAge, bool isProfileImage)
        {
            if (isProfileImage)
            {
                Deer? deer = LoadDbSet().Where(deer => deer.Id == deerId).FirstOrDefault();

                if (deer != null)
                {
                    deer.AgeOfBuckDisplayed = newAge;
                }
                                                        
            }
            else
            {
                Media? media = _reviewPlatformDBContext.Media.Where(media => media.BlobId == url && media.DeerId == deerId).FirstOrDefault();
                if(media != null)
                {
                    media.AgeOfBuckDisplayed = newAge;
                }
            }

            return _reviewPlatformDBContext.SaveChanges() > 0;

        }

        public bool RemoveImage(Guid deerId, string imageUrl)
        {
            Deer? deer = LoadDbSet().Where(deer => deer.Id == deerId)
                       .Include(deer => deer.Media)
                       .FirstOrDefault();

            if(deer != null)
            {
                Media? media = _reviewPlatformDBContext.Media.Where(media => media.BlobId == imageUrl && media.DeerId == deerId).FirstOrDefault();

                if(media != null)
                {
                    _reviewPlatformDBContext.Remove(media);
                    _reviewPlatformDBContext.SaveChanges();
                    return true;
                }

                return false;
            }

            return false;
        }

    }
}
