using ReviewPlatformAPI.Constants;
using ReviewPlatformAPI.Entities;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ReviewPlatformAPI.Utils
{
    public class AuthHelper
    {
        private readonly CWDBreedingContext _reviewPlatformDbContext;
        public AuthHelper(CWDBreedingContext reviewPlatformDbContext)
        {
            _reviewPlatformDbContext = reviewPlatformDbContext;
        }

        public bool CheckCredentials(string username, string password, string userLoginType)
        {
            // Refactored to check which type of login needs to happen
            if (userLoginType == UserLoginTypes.User)
            {
                User user = _reviewPlatformDbContext.Users.Include(user => user.LoginData).FirstOrDefault(client => client.Email == username);
                if (user != null && Sha256(password, Convert.FromBase64String(user.LoginData!.Salt)) == user.LoginData.Password)
                {
                    return true;
                }
            }
            else if (userLoginType == UserLoginTypes.Ranch)
            {
                Ranch ranch = _reviewPlatformDbContext.Ranches.Include(ranch => ranch.LoginData).FirstOrDefault(x => x.Email == username);
                if (ranch != null && Sha256(password, Convert.FromBase64String(ranch.LoginData!.Salt)) == ranch.LoginData.Password)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Generate new salt for hashing password
        /// </summary>
        /// <returns></returns>
        public byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }

        /// <summary>
        /// Hash a password/token with salt
        /// </summary>
        /// <param name="randomString"></param>
        /// <param name="salt"></param>
        /// Source: https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-5.0
        /// <returns>Hashed string.</returns>
        public string Sha256(string randomString, byte[] salt)
        {
            // derive a 256-bit subkey (use HMACSHA256 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: randomString,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8)
            );

            return hashed;
        }

        public string[] decodeAuth(string encodedAuth)
        {

            // Basic user:pass
            string decodedAuth = Encoding.UTF8.GetString(Convert.FromBase64String(encodedAuth.Split(" ")[1]));
            string[] authParts = decodedAuth.Split(":");

            if (authParts.Length < 2)
            {
                throw new Exception("Unathorized Auth Request");
            }

            foreach (string part in authParts)
            {
                if (string.IsNullOrEmpty(part))
                {
                    throw new Exception("Unathorized Auth Request");
                }
            }

            return authParts;
        }
    }
}
