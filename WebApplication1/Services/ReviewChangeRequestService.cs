using ReviewPlatformAPI.Entities;
using ReviewPlatformAPI.Models;
using ReviewPlatformAPI.Repos;

namespace ReviewPlatformAPI.Services
{
    public class ReviewChangeRequestService : BaseService<ReviewChangeRequestModel, ReviewChangeRequest>
    {
        private readonly ReviewChangeRequestRepo _reviewChangeRequestRepo;

        public ReviewChangeRequestService(ReviewChangeRequestRepo reviewChangeRequestRepo)
        {
            _reviewChangeRequestRepo = reviewChangeRequestRepo;
        }

        public override ReviewChangeRequest ConverToEntityForAdd(ReviewChangeRequestModel model)
        {
            return new ReviewChangeRequest
            {
                ReviewId = model.ReviewId,
                ChangeRequestId = model.ChangeRequestId,
                Status = "ACTIVE",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
            };
        }

        public override void CopyDataForUpdate(ReviewChangeRequest entity, ReviewChangeRequestModel model)
        {
            entity.UpdateDate = DateTime.Now;
            entity.Status = model.Status ?? entity.Status;
        }

        public override ReviewChangeRequestModel CreateModelForIndividualLookup(ReviewChangeRequest entity)
        {
            return new ReviewChangeRequestModel
            {
                Id = entity.Id,
                ReviewId = entity.ReviewId,
                ChangeRequestId = entity.ChangeRequestId,
                Status = entity.Status,
                CreateDate = entity.CreateDate,
                UpdateDate = entity.UpdateDate
            };
        }

        public override ReviewChangeRequestModel CreateModelForListLookup(ReviewChangeRequest entity)
        {
            return new ReviewChangeRequestModel
            {
                Id = entity.Id,
                ReviewId = entity.ReviewId,
                ChangeRequestId = entity.ChangeRequestId,
                Status = entity.Status,
                CreateDate = entity.CreateDate,
                UpdateDate = entity.UpdateDate
            };
        }

        public override BaseRepo<ReviewChangeRequest> LoadRepo()
        {
            return _reviewChangeRequestRepo;
        }
    }
}

