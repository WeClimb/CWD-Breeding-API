using Microsoft.EntityFrameworkCore;
using ReviewPlatformAPI.Entities;

namespace ReviewPlatformAPI.Repos
{
    public class ServiceProviderReviewRepo : BaseRepo<ServiceProviderReview>
    {
        public ServiceProviderReviewRepo(ReviewPlatformDBContext reviewPlatformDBContext) : base(reviewPlatformDBContext)
        {
        }

        public override DbSet<ServiceProviderReview> LoadDbSet()
        {
            return _reviewPlatformDBContext.ServiceProvidersReviews;
        }
    }
}
