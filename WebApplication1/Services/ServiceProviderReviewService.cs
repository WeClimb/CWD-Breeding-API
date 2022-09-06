using ReviewPlatformAPI.Entities;
using ReviewPlatformAPI.Models;
using ReviewPlatformAPI.Repos;

namespace ReviewPlatformAPI.Services
{
    public class ServiceProviderReviewService : BaseService<ServiceProviderReviewModel, ServiceProviderReview>
    {
        private readonly ServiceProviderReviewRepo _serviceProviderReviewRepo;

        public ServiceProviderReviewService(ServiceProviderReviewRepo serviceProviderReviewRepo)
        {
            _serviceProviderReviewRepo = serviceProviderReviewRepo;
        }
        public override ServiceProviderReview ConverToEntityForAdd(ServiceProviderReviewModel model)
        {
            return new ServiceProviderReview
            {
                Status = "ACTIVE",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
            };
        }

        public override void CopyDataForUpdate(ServiceProviderReview entity, ServiceProviderReviewModel model)
        {
            entity.Status = model.Status;
            entity.CreateDate = model.CreateDate;
            entity.UpdateDate = model.UpdateDate;
            entity.Id = model.Id;
        }

        public override ServiceProviderReviewModel CreateModelForIndividualLookup(ServiceProviderReview entity)
        {
            return new ServiceProviderReviewModel
            {
                Id = entity.Id,
                Status = entity.Status
            };
        }

        public override ServiceProviderReviewModel CreateModelForListLookup(ServiceProviderReview entity)
        {
            return new ServiceProviderReviewModel
            {
                Id = entity.Id,
                Status = entity.Status
            };
        }

        public override BaseRepo<ServiceProviderReview> LoadRepo()
        {
            return _serviceProviderReviewRepo;
        }
    }
}
