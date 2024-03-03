using Microsoft.EntityFrameworkCore;
using ReviewPlatformAPI.Entities;

namespace ReviewPlatformAPI.Repos
{
    public class RanchRepo : BaseRepo<Ranch>
    {
        public RanchRepo(CWDBreedingContext cwdBreedingContext) : base(cwdBreedingContext)
        {
        }

        public override DbSet<Ranch> LoadDbSet()
        {
            return _reviewPlatformDBContext.Ranches;
        }

        public Ranch? CheckIfEmailExists(string emailToAdd)
        {
            return LoadDbSet().Where(ranch => ranch.Email == emailToAdd).FirstOrDefault();
        }

        /// <summary>
        /// We want JWT Auth to not track the entity since it will never be updated during authentication
        /// </summary>
        /// <param name="id"></param>
        /// <returns> return the entity just to prove it exists during authentication.</returns>
        public Ranch GetByNoTrackingId(Guid id)
        {
            return LoadDbSet().AsNoTracking().FirstOrDefault(ranch => ranch.Id == id)!;
        }

        public Ranch GetAuthRanch(string userName)
        {
            return _reviewPlatformDBContext.Ranches.SingleOrDefault(x => x.Email == userName) ?? throw new Exception("Authentication Failed");
        }

        public ChangePassword GetChangePasswordRequest(Guid id)
        {
            return _reviewPlatformDBContext.ChangePasswords.Include(changePassword => changePassword.Ranch)
                                                           .ThenInclude(x => x!.LoginData)
                                                           .FirstOrDefault(changePassword => changePassword.Id == id) ?? throw new Exception("TODO: ERROR");
        }

        public Ranch GetRanchByEmail(string email)
        {
            return _reviewPlatformDBContext.Ranches.FirstOrDefault(client => client.Email == email) ?? throw new Exception("TODO: ERROR");
        }

        public List<Ranch> GetRanchesByName(string? name, string? ownerFirstName, string? ownerLastName)
        {
            var query = _reviewPlatformDBContext.Ranches.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(ranch => ranch.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(ownerFirstName))
            {
                query = query.Where(ranch => ranch.OwnerFirstName.Contains(ownerFirstName));
            }

            if (!string.IsNullOrEmpty(ownerLastName))
            {
                query = query.Where(ranch => ranch.OwnerlastName.Contains(ownerLastName));
            }

            return query.ToList();
        }

    }
}
