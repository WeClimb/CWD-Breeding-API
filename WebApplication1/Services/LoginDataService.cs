using ReviewPlatformAPI.Entities;
using ReviewPlatformAPI.Models;
using ReviewPlatformAPI.Repos;

namespace ReviewPlatformAPI.Services
{
    public class LoginDataService : BaseService<LoginDataModel, LoginData>
    {
        private readonly LoginDataRepo _loginDataRepo;

        public LoginDataService(LoginDataRepo loginDataRepo)
        {
            _loginDataRepo = loginDataRepo;
        }
        public override LoginData ConverToEntityForAdd(LoginDataModel model)
        {
            return new LoginData
            {
                Status = "ACTIVE",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
            };
        }

        public override void CopyDataForUpdate(LoginData entity, LoginDataModel model)
        {
            entity.Status = model.Status;
            entity.CreateDate = model.CreateDate;
            entity.UpdateDate = model.UpdateDate;
            entity.LastLoginDate = model.LastLoginDate;
            entity.Password = model.Password;
            entity.Salt = model.Salt;
            entity.Id = model.Id;
        }

        public override LoginDataModel CreateModelForIndividualLookup(LoginData entity)
        {
            return new LoginDataModel
            {
                Status = entity.Status,
                CreateDate = entity.CreateDate,
                UpdateDate = entity.UpdateDate,
                LastLoginDate = entity.LastLoginDate,
                Password = entity.Password,
                Salt = entity.Salt,
                Id = entity.Id,
            };
        }

        public override LoginDataModel CreateModelForListLookup(LoginData entity)
        {
            return new LoginDataModel
            {
                Status = entity.Status,
                CreateDate = entity.CreateDate,
                UpdateDate = entity.UpdateDate,
                LastLoginDate = entity.LastLoginDate,
                Password = entity.Password,
                Salt = entity.Salt,
                Id = entity.Id,
            };
        }

        public override BaseRepo<LoginData> LoadRepo()
        {
            return _loginDataRepo;
        }
    }
}
