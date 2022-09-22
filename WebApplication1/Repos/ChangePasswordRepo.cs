using Microsoft.EntityFrameworkCore;
using ReviewPlatformAPI.Entities;

namespace ReviewPlatformAPI.Repos
{
    public class ChangePasswordRepo : BaseRepo<ChangePassword>
    {
        public ChangePasswordRepo(CWDBreedingContext reviewPlatformDBContext) : base(reviewPlatformDBContext)
        {
        }

        public override DbSet<ChangePassword> LoadDbSet()
        {
            return _reviewPlatformDBContext.ChangePasswords;
        }
    }
}
