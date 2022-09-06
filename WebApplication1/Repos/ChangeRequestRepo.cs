using Microsoft.EntityFrameworkCore;
using ReviewPlatformAPI.Entities;

namespace ReviewPlatformAPI.Repos
{
    public class ChangeRequestRepo : BaseRepo<ChangeRequest>
    {
        public ChangeRequestRepo(ReviewPlatformDBContext reviewPlatformDBContext) : base(reviewPlatformDBContext)
        {
        }

        public override DbSet<ChangeRequest> LoadDbSet()
        {
            return _reviewPlatformDBContext.ChangeRequests;
        }
    }
}
