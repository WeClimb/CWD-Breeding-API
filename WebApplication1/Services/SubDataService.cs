using ReviewPlatformAPI.Entities;
using ReviewPlatformAPI.Models;
using ReviewPlatformAPI.Repos;

namespace ReviewPlatformAPI.Services
{
    public class SubDataService : BaseService<SubDataModel, SubData>
    {
        private readonly SubDataRepo _subDataRepo;

        public SubDataService(SubDataRepo subDataRepo)
        {
            _subDataRepo = subDataRepo;
        }
        public override SubData ConverToEntityForAdd(SubDataModel model)
        {
            return new SubData
            {
                Status = "ACTIVE",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
            };
        }

        public override void CopyDataForUpdate(SubData entity, SubDataModel model)
        {
            entity.Status = model.Status;
            entity.CreateDate = model.CreateDate;
            entity.UpdateDate = model.UpdateDate;
            entity.Id = model.Id;
        }

        public override SubDataModel CreateModelForIndividualLookup(SubData entity)
        {
            return new SubDataModel
            {
                Id = entity.Id,
                Status = entity.Status
            };
        }

        public override SubDataModel CreateModelForListLookup(SubData entity)
        {
            return new SubDataModel
            {
                Id = entity.Id,
                Status = entity.Status
            };
        }

        public override BaseRepo<SubData> LoadRepo()
        {
            return _subDataRepo;
        }
    }
}
