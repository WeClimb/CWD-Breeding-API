using Microsoft.EntityFrameworkCore;
using ReviewPlatformAPI.Entities;

namespace ReviewPlatformAPI.Repos
{
    public class ReviewRepo : BaseRepo<Review>
    {
        public ReviewRepo(ReviewPlatformDBContext reviewPlatformDBContext) : base(reviewPlatformDBContext)
        {
        }

        public override DbSet<Review> LoadDbSet()
        {
            return _reviewPlatformDBContext.Reviews;
        }
    }
}
