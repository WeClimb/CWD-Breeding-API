using Microsoft.EntityFrameworkCore;
using ReviewPlatformAPI.Entities;

namespace ReviewPlatformAPI.Repos
{
    public class SubDataRepo : BaseRepo<SubData>
    {
        public SubDataRepo(ReviewPlatformDBContext reviewPlatformDBContext) : base(reviewPlatformDBContext)
        {
        }

        public override DbSet<SubData> LoadDbSet()
        {
            return _reviewPlatformDBContext.SubData;
        }
    }
}
