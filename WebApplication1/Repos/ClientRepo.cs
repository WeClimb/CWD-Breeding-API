using Microsoft.EntityFrameworkCore;
using ReviewPlatformAPI.Entities;

namespace ReviewPlatformAPI.Repos
{
    public class ClientRepo : BaseRepo<Client>
    {
        public ClientRepo(ReviewPlatformDBContext reviewPlatformDBContext) : base(reviewPlatformDBContext)
        {
        }

        public override DbSet<Client> LoadDbSet()
        {
            return _reviewPlatformDBContext.Clients;
        }

        public Client? CheckIfEmailExists(string emailToAdd)
        {
            return LoadDbSet().Where(client => client.Email.ToLower() == emailToAdd.ToLower()).FirstOrDefault();
        }

        /// <summary>
        /// We want JWT Auth to not track the entity since it will never be updated during authentication
        /// </summary>
        /// <param name="id"></param>
        /// <returns> return the entity just to prove it exists during authentication.</returns>
        public Client GetByNoTrackingId(Guid id)
        {
            return LoadDbSet().AsNoTracking().FirstOrDefault(client => client.Id == id)!;
        }

        public Client GetAuthClient(string userName)
        {
            return _reviewPlatformDBContext.Clients.SingleOrDefault(x => x.Email == userName) ?? throw new Exception("TODO: ERROR");
        }

        public ChangePassword GetChangePasswordRequest(Guid id)
        {
            return _reviewPlatformDBContext.ChangePasswords.Include(changePassword => changePassword.Client)
                                                           .ThenInclude(x => x!.LoginData)
                                                           .FirstOrDefault(changePassword => changePassword.Id == id) ?? throw new Exception("TODO: ERROR");
        }

        public Client GetClientByEmail(string email)
        {
            return _reviewPlatformDBContext.Clients.FirstOrDefault(client => client.Email.ToLower() == email.ToLower()) ?? throw new Exception("TODO: ERROR");
        }
    }
}
