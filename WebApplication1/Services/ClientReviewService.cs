using ReviewPlatformAPI.Entities;
using ReviewPlatformAPI.Models;
using ReviewPlatformAPI.Repos;

namespace ReviewPlatformAPI.Services
{
    public class ClientReviewService : BaseService<ClientReviewModel, ClientReview>
    {
        private readonly ClientReviewRepo _clientReviewRepo;

        public ClientReviewService(ClientReviewRepo clientReviewRepo)
        {
            _clientReviewRepo = clientReviewRepo;
        }
        public override ClientReview ConverToEntityForAdd(ClientReviewModel model)
        {
            return new ClientReview
            {
                Status = "ACTIVE",
                ReviewId = model.ReviewId,
                ClientId = model.ClientId,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
            };
        }

        public override void CopyDataForUpdate(ClientReview entity, ClientReviewModel model)
        {
            entity.UpdateDate = DateTime.Now;
            entity.Status = model.Status;
        }

        public override ClientReviewModel CreateModelForIndividualLookup(ClientReview entity)
        {
            return new ClientReviewModel
            {
                Status = entity.Status,
                ReviewId = entity.ClientId,
                ClientId = entity.ClientId,
                CreateDate = entity.CreateDate,
                UpdateDate = entity.UpdateDate,
            };
        }

        public override ClientReviewModel CreateModelForListLookup(ClientReview entity)
        {
            return new ClientReviewModel
            {
                Status = entity.Status,
                ReviewId = entity.ClientId,
                ClientId = entity.ClientId,
                CreateDate = entity.CreateDate,
                UpdateDate = entity.UpdateDate,
            };
        }

        public override BaseRepo<ClientReview> LoadRepo()
        {
            return _clientReviewRepo;
        }
    }
}
