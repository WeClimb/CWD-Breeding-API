using ReviewPlatformAPI.Entities;
using ReviewPlatformAPI.Models;
using ReviewPlatformAPI.Repos;

namespace ReviewPlatformAPI.Services
{
    public class ChangePasswordService : BaseService<ChangePasswordModel, ChangePassword>
    {
        private readonly ChangePasswordRepo _ChangePasswordRepo;

        public ChangePasswordService(ChangePasswordRepo ChangePasswordRepo)
        {
            _ChangePasswordRepo = ChangePasswordRepo;
        }
        public override ChangePassword ConverToEntityForAdd(ChangePasswordModel model)
        {
            return new ChangePassword
            {
                Id = Guid.NewGuid(),
                ClientId = model.ClientId,
                ServiceProviderId = model.ServiceProviderId,
                ExpirationDate = DateTime.UtcNow.AddDays(1),
                Status = "ACTIVE",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
            };
        }

        public override void CopyDataForUpdate(ChangePassword entity, ChangePasswordModel model)
        {
            entity.Status = model.Status;
            entity.UpdateDate = DateTime.Now;
        }

        public override ChangePasswordModel CreateModelForIndividualLookup(ChangePassword entity)
        {
            return new ChangePasswordModel
            {
                Id = entity.Id,
                ClientId = entity.ClientId,
                ServiceProviderId = entity.ServiceProviderId,
                ExpirationDate = entity.ExpirationDate,
                Status = entity.Status,
                CreateDate = entity.CreateDate,
                UpdateDate = entity.UpdateDate,
            };
        }

        public override ChangePasswordModel CreateModelForListLookup(ChangePassword entity)
        {
            return new ChangePasswordModel
            {
                Id = entity.Id,
                ClientId = entity.ClientId,
                ServiceProviderId = entity.ServiceProviderId,
                ExpirationDate = entity.ExpirationDate,
                Status = entity.Status,
                CreateDate = entity.CreateDate,
                UpdateDate = entity.UpdateDate,
            };
        }

        public override BaseRepo<ChangePassword> LoadRepo()
        {
            return _ChangePasswordRepo;
        }
    }
}

