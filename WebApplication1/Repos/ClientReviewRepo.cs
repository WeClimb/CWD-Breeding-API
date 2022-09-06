using Microsoft.EntityFrameworkCore;
using ReviewPlatformAPI.Entities;

namespace ReviewPlatformAPI.Repos
{
    public class ClientReviewRepo : BaseRepo<ClientReview>
    {
        public ClientReviewRepo(ReviewPlatformDBContext reviewPlatformDBContext) : base(reviewPlatformDBContext)
        {
        }

        public override DbSet<ClientReview> LoadDbSet()
        {
            return _reviewPlatformDBContext.ClientsReviews;
        }
    }
}
