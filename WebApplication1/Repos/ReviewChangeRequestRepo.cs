using Microsoft.EntityFrameworkCore;
using ReviewPlatformAPI.Entities;

namespace ReviewPlatformAPI.Repos
{
    public class ReviewChangeRequestRepo : BaseRepo<ReviewChangeRequest>
    {
        public ReviewChangeRequestRepo(ReviewPlatformDBContext reviewPlatformDBContext) : base(reviewPlatformDBContext)
        {
        }

        public override DbSet<ReviewChangeRequest> LoadDbSet()
        {
            return _reviewPlatformDBContext.ReviewsChangeRequests;
        }
    }
}
