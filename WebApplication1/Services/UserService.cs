using ReviewPlatformAPI.Models;
using ReviewPlatformAPI.Entities;
using ReviewPlatformAPI.Repos;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using ReviewPlatformAPI.Utils;
using ReviewPlatformAPI.Constants;

namespace ReviewPlatformAPI.Services
{
    public class UserService : BaseService<UserModel, User>
    {
        private readonly UserRepo _userRepo;
        private readonly LoginDataService _loginDataService;
        private readonly IConfiguration _configuration;
        private readonly AuthHelper _authHelper;
        private readonly ChangePasswordService _changePasswordService;
        private readonly EmailService _emailService;
        public UserService(UserRepo userRepo, 
                            LoginDataService loginDataService, 
                            IConfiguration configuration, 
                            AuthHelper authHelper,
                            ChangePasswordService changerPasswordService,
                            EmailService emailService)
        {
            _userRepo = userRepo;
            _loginDataService = loginDataService;
            _configuration = configuration;
            _authHelper = authHelper;
            _changePasswordService = changerPasswordService;
            _emailService = emailService;
        }

        //TODO: Figure out a way if there is any fail delete everything that happened!
        protected override void AdditionalPreAddLogic(User entity)
        {
            User? user = _userRepo.CheckIfEmailExists(entity.Email);

            if(user == null)
            {
                LoginDataModel loginData = new LoginDataModel();
                entity.LoginDataId = _loginDataService.Create(loginData);
            }
            else
            {
                throw new Exception("Failed to add User");
            }
        }

        protected override void AdditonalPostAddLogic(User entity)
        {
            var emailSuccess = ForgotPassword(entity.Email);
            if (!emailSuccess)
            {
                throw new Exception("Email failed to send.");
            }
        }

        public override User ConverToEntityForAdd(UserModel model)
        {
            return new User
            {
                Status = "ACTIVE",
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                City = model.City,
                State = model.State,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
            };
        }

        public override void CopyDataForUpdate(User entity, UserModel model)
        {
            if(model.FirstName != null && model.FirstName.Length != 0)
            {
                entity.FirstName = model.FirstName;
            }
            if (model.LastName != null && model.LastName.Length != 0)
            {
                entity.LastName = model.LastName;
            }
            if (model.Email != null && model.Email.Length != 0)
            {
                entity.Email = model.Email;
            }
            if (model.City != null && model.City.Length != 0)
            {
                entity.City = model.City;
            }
            if (model.State != null && model.State.Length != 0)
            {
                entity.State = model.State;
            }

            entity.Status = model.Status ?? entity.Status;
            entity.UpdateDate = DateTime.Now;
        }

        public override UserModel CreateModelForIndividualLookup(User entity)
        {
            return new UserModel
            {
                 Id = entity.Id,
                 FirstName = entity.FirstName,
                 LastName = entity.LastName,
                 Email = entity.Email,
                 Status = entity.Status,
                 City = entity.City,
                 State = entity.State,
                 LoginDataId = entity.LoginDataId,
                 CreateDate = entity.CreateDate,
                 UpdateDate = entity.UpdateDate
            };
        }

        public override UserModel CreateModelForListLookup(User entity)
        {
            return new UserModel
            {
                Status = entity.Status,
                Email = entity.Email,
                LoginDataId = entity.LoginDataId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                City = entity.City,
                State = entity.State,
                CreateDate = entity.CreateDate,
                UpdateDate = entity.UpdateDate
            };
        }

        public User GetByIDNoTracking(Guid id)
        {
            return _userRepo.GetByNoTrackingId(id);
        }
        public User Login(string encodedAuthRequest)
        {
            string[] parsedAuthRequest = _authHelper.decodeAuth(encodedAuthRequest);
            string username = parsedAuthRequest[0];
            string password = parsedAuthRequest[1];

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new Exception("TODO: ERROR");
            }

            User user = _userRepo.GetAuthUser(username);

            if (user == null)
            {
                throw new Exception("TODO: ERROR");
            }

            if (!_authHelper.CheckCredentials(username, password, UserLoginTypes.User))
            {
                throw new Exception("TODO: ERROR");
            }

            return user;
        }

            public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool ChangePassword(Guid changePasswordId, string password)
        {
            ChangePassword changePassword = _userRepo.GetChangePasswordRequest(changePasswordId);
            User user = changePassword.User;

            if (changePassword == null || DateTime.Now.CompareTo(changePassword.ExpirationDate) > 0)
            {
                return false;
            }

            if (user == null)
            {
                return false;
            }

            byte[] newSalt = _authHelper.GenerateSalt();
            user.LoginData!.Password = _authHelper.Sha256(password, newSalt);
            user.LoginData!.Salt = Convert.ToBase64String(newSalt);

            _userRepo.SaveChanges();

            return true;
        }

        //TODO: Once we get front end set up and figure out urls finish setting this up
        public bool ForgotPassword(string email)
        {
            User user = _userRepo.GetUserByEmail(email);

            if (user == null)
            {
                return false;
            }

            ChangePasswordModel changePasswordModel = new ChangePasswordModel();
            changePasswordModel.UserId = user.Id;

            string changePasswordId = _changePasswordService.Create(changePasswordModel).ToString();

            if (changePasswordId == null)
            {
                return false;
            }

            //string currentHost = _healthPossibleDbContext.Settings.FirstOrDefault(x => x.SettingKey == "CUSTOMER_URL").SettingValue;
            string currentHost = _configuration["CurrentHost"];
            string Url = "";

            Url = $"{currentHost}{string.Concat("change-password/", changePasswordId.ToString())}";
            
            bool emailStatus = _emailService.SendEmail(
                user.Email,
                EmailConstants.ChangePasswordSubject,
                string.Format(EmailConstants.ChangePasswordBody, user.FirstName, Url),
                null
            );

            return emailStatus;
        }


        public override BaseRepo<User> LoadRepo()
        {
            return _userRepo;
        }
    }
}
