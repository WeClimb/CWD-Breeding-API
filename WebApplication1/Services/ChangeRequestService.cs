using ReviewPlatformAPI.Entities;
using ReviewPlatformAPI.Models;
using ReviewPlatformAPI.Repos;

namespace ReviewPlatformAPI.Services
{
    public class ChangeRequestService : BaseService<ChangeRequestModel, ChangeRequest>
    {
        private readonly ChangeRequestRepo _changeRequestRepo;

        public ChangeRequestService(ChangeRequestRepo ChangeRequestRepo)
        {
            _changeRequestRepo = ChangeRequestRepo;
        }
        public override ChangeRequest ConverToEntityForAdd(ChangeRequestModel model)
        {
            return new ChangeRequest
            {
                Reason = model.Reason!,
                Status = "ACTIVE",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
            };
        }

        public override void CopyDataForUpdate(ChangeRequest entity, ChangeRequestModel model)
        {
            entity.UpdateDate = DateTime.Now;
            entity.Reason = model.Reason ?? entity.Reason;
        }

        public override ChangeRequestModel CreateModelForIndividualLookup(ChangeRequest entity)
        {
            return new ChangeRequestModel
            {
                Id = entity.Id,
                Reason = entity.Reason!,
                Status = entity.Status,
                CreateDate = entity.CreateDate,
                UpdateDate = entity.UpdateDate,
            };
        }

        public override ChangeRequestModel CreateModelForListLookup(ChangeRequest entity)
        {
            return new ChangeRequestModel
            {
                Id = entity.Id,
                Reason = entity.Reason!,
                Status = entity.Status,
                CreateDate = entity.CreateDate,
                UpdateDate = entity.UpdateDate,
            };
        }

        public override BaseRepo<ChangeRequest> LoadRepo()
        {
            return _changeRequestRepo;
        }
    }
}

