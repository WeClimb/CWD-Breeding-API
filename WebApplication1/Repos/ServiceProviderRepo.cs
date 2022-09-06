using Microsoft.EntityFrameworkCore;
using ReviewPlatformAPI.Entities;
using ServiceProvider = ReviewPlatformAPI.Entities.ServiceProvider;

namespace ReviewPlatformAPI.Repos
{
    public class ServiceProviderRepo : BaseRepo<Entities.ServiceProvider>
    {
        public ServiceProviderRepo(ReviewPlatformDBContext reviewPlatformDBContext) : base(reviewPlatformDBContext)
        {
        }

        public override DbSet<ServiceProvider> LoadDbSet()
        {
            return _reviewPlatformDBContext.ServiceProviders;
        }

        public ServiceProvider? CheckIfEmailExists(string emailToAdd)
        {
            return LoadDbSet().Where(serviceProvider => serviceProvider.Email == emailToAdd).FirstOrDefault();
        }

        /// <summary>
        /// We want JWT Auth to not track the entity since it will never be updated during authentication
        /// </summary>
        /// <param name="id"></param>
        /// <returns> return the entity just to prove it exists during authentication.</returns>
        public ServiceProvider GetByNoTrackingId(Guid id)
        {
            return LoadDbSet().AsNoTracking().FirstOrDefault(serviceProvider => serviceProvider.Id == id)!;
        }

        public ServiceProvider GetAuthClient(string userName)
        {
            return _reviewPlatformDBContext.ServiceProviders.SingleOrDefault(x => x.Email == userName) ?? throw new Exception("TODO: ERROR");
        }

        public ChangePassword GetChangePasswordRequest(Guid id)
        {
            return _reviewPlatformDBContext.ChangePasswords.Include(changePassword => changePassword.ServiceProvider)
                                                           .ThenInclude(x => x!.LoginData)
                                                           .FirstOrDefault(changePassword => changePassword.Id == id) ?? throw new Exception("TODO: ERROR");
        }

        public ServiceProvider GetServiceProviderByEmail(string email)
        {
            return _reviewPlatformDBContext.ServiceProviders.FirstOrDefault(client => client.Email == email) ?? throw new Exception("TODO: ERROR");
        }
    }
}
