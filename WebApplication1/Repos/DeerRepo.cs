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

        public List<Deer> GetAllFiltered(bool isApproved,string? deerName, string? ranchName, string? codon, decimal? gebv,int? age,int? sciScore)
        {
            IQueryable<Deer> query = LoadDbSet().Where(deer => deer.IsApproved == isApproved)
                                          .Where(deer => deer.Name.Contains(deerName))
                                          .Where(deer => deer.Ranch.Name.Contains(ranchName))
                                          .Where(deer => deer.Codon.Contains(codon))
                                          .Where(deer => deer.Status.ToLower() != "denied");

            if (gebv != null) {
                query = query.Where(deer => deer.Gebu <= gebv);
            }

            if(age != null)
            {
                query = query.Where(deer => deer.Age.ToString().Contains(age.ToString()));
            }

            if(sciScore != null)
            {
                query = query.Where(deer => deer.SciScore >= sciScore);
            }

            return query.Include(deer => deer.Ranch)
                        .Include(deer => deer.LevelOneRelationships)
                        .Include(deer => deer.LevelTwoRelationships)
                        .Include(deer => deer.LevelThreeRelationships)
                        .ToList();
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

        public List<Deer> GetAll(bool isPending)
        {
            return LoadDbSet().Where(deer => deer.IsApproved == isPending).Where(deer => deer.Status.ToLower() != "denied")
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
    }
}
