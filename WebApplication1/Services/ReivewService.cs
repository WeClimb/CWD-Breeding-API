using ReviewPlatformAPI.Entities;
using ReviewPlatformAPI.Models;
using ReviewPlatformAPI.Repos;

namespace ReviewPlatformAPI.Services
{
    public class ReviewService : BaseService<ReviewModel, Review>
    {
        private readonly ReviewRepo _reviewRepo;

        public ReviewService(ReviewRepo reviewRepo)
        {
            _reviewRepo = reviewRepo;
        }
        public override Review ConverToEntityForAdd(ReviewModel model)
        {
            return new Review
            {
                Id = Guid.NewGuid(),
                Service = model.Service,
                Description = model.Description,
                Rating = model.Rating,
                FirstName = model.FirstName,
                LastName = model.LastName,
                City = model.City,
                State = model.State,
                Status = "ACTIVE",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
            };
        }

        public override void CopyDataForUpdate(Review entity, ReviewModel model)
        {
            entity.Status = model.Status ?? entity.Status;
            entity.UpdateDate = DateTime.Now;
            entity.Description = model.Description ?? entity.Description;
            entity.Rating = model.Rating;
            entity.FirstName = model.FirstName ?? entity.FirstName;
            entity.LastName = model.LastName ?? entity.LastName;
            entity.City = model.City ?? entity.City ;
            entity.State = model.State ?? entity.State;
            entity.Service = model.Service ?? entity.Service;
        }

        public override ReviewModel CreateModelForIndividualLookup(Review entity)
        {
            return new ReviewModel
            {
                Id = entity.Id,
                Service = entity.Service,
                Description = entity.Description,
                Rating = entity.Rating,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                City = entity.City,
                State = entity.State,
                CreateDate = entity.CreateDate,
                UpdateDate = entity.UpdateDate,
                Status = entity.Status
            };
        }

        public override ReviewModel CreateModelForListLookup(Review entity)
        {
            return new ReviewModel
            {
                Id = entity.Id,
                Service = entity.Service,
                Description = entity.Description,
                Rating = entity.Rating,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                City = entity.City,
                State = entity.State,
                CreateDate = entity.CreateDate,
                UpdateDate = entity.UpdateDate,
                Status = entity.Status
            };
        }

        public override BaseRepo<Review> LoadRepo()
        {
            return _reviewRepo;
        }
    }
}
