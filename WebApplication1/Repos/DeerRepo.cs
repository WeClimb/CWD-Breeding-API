using CWDBreedingAPI.Models.Non_EntityModels;
using Microsoft.EntityFrameworkCore;
using ReviewPlatformAPI.Entities;
using ReviewPlatformAPI.Models.Non_EntityModels;

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
                throw new Exception("Family Createion Failed");
            }
       
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
            return LoadDbSet().Where(deer => deer.IsApproved == isPending)
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
