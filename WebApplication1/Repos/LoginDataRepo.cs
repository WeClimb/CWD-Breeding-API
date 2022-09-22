using Microsoft.EntityFrameworkCore;
using ReviewPlatformAPI.Entities;

namespace ReviewPlatformAPI.Repos
{
    public class LoginDataRepo : BaseRepo<LoginData>
    {
        public LoginDataRepo(CWDBreedingContext reviewPlatformDBContext) : base(reviewPlatformDBContext)
        {
        }

        public override DbSet<LoginData> LoadDbSet()
        {
            return _reviewPlatformDBContext.LoginData;
        }
    }
}
