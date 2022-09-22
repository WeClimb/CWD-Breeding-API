using Microsoft.EntityFrameworkCore;
using ReviewPlatformAPI.Entities;

namespace ReviewPlatformAPI.Repos
{
    public class UserRepo : BaseRepo<User>
    {
        public UserRepo(CWDBreedingContext cwdBreedingContext) : base(cwdBreedingContext)
        {
        }

        public override DbSet<User> LoadDbSet()
        {
            return _reviewPlatformDBContext.Users;
        }

        public User? CheckIfEmailExists(string emailToAdd)
        {
            return LoadDbSet().Where(user => user.Email == emailToAdd).FirstOrDefault();
        }

        /// <summary>
        /// We want JWT Auth to not track the entity since it will never be updated during authentication
        /// </summary>
        /// <param name="id"></param>
        /// <returns> return the entity just to prove it exists during authentication.</returns>
        public User GetByNoTrackingId(Guid id)
        {
            return LoadDbSet().AsNoTracking().FirstOrDefault(user => user.Id == id)!;
        }

        public User GetAuthUser(string userName)
        {
            return _reviewPlatformDBContext.Users.SingleOrDefault(x => x.Email == userName) ?? throw new Exception("TODO: ERROR");
        }

        public ChangePassword GetChangePasswordRequest(Guid id)
        {
            return _reviewPlatformDBContext.ChangePasswords.Include(changePassword => changePassword.User)
                                                           .ThenInclude(x => x!.LoginData)
                                                           .FirstOrDefault(changePassword => changePassword.Id == id) ?? throw new Exception("TODO: ERROR");
        }

        public User GetUserByEmail(string email)
        {
            return _reviewPlatformDBContext.Users.FirstOrDefault(client => client.Email == email) ?? throw new Exception("TODO: ERROR");
        }
    }
}
