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
    public class RanchService : BaseService<RanchModel, Ranch>
    {
        private readonly RanchRepo _ranchRepo;
        private readonly LoginDataService _loginDataService;
        private readonly SubDataService _subDataService;
        private readonly IConfiguration _configuration;
        private readonly AuthHelper _authHelper;
        private readonly ChangePasswordService _changePasswordService;
        private readonly EmailService _emailService;
        public RanchService(RanchRepo RanchRepo,
                            LoginDataService loginDataService,
                            SubDataService subDataService,
                            IConfiguration configuration,
                            AuthHelper authHelper,
                            ChangePasswordService changerPasswordService,
                            EmailService emailService)
        {
            _ranchRepo = RanchRepo;
            _loginDataService = loginDataService;
            _subDataService = subDataService;
            _configuration = configuration;
            _authHelper = authHelper;
            _changePasswordService = changerPasswordService;
            _emailService = emailService;
        }

        //TODO: Figure out a way if there is any fail delete everything that happened!
        protected override void AdditionalPreAddLogic(Ranch entity)
        {
            Ranch? ranch = _ranchRepo.CheckIfEmailExists(entity.Email);

            if (ranch == null)
            {
                LoginDataModel loginData = new LoginDataModel();
                entity.LoginDataId = _loginDataService.Create(loginData);
            }
            else
            {
                throw new Exception("Failed to add Ranch");
            }
        }

        protected override void AdditonalPostAddLogic(Ranch entity)
        {
            var emailSuccess = ForgotPassword(entity.Email);
            if (!emailSuccess)
            {
                throw new Exception("Email failed to send.");
            }
        }

        public override Ranch ConverToEntityForAdd(RanchModel model)
        {
            return new Ranch
            {
                Status = "ACTIVE",
                Email = model.Email,
                OwnerFirstName = model.OwnerFirstName,
                OwnerlastName = model.OwnerlastName,
                City = model.City,
                State = model.State,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
            };
        }

        public override void CopyDataForUpdate(Ranch entity, RanchModel model)
        {
            if (model.OwnerFirstName != null && model.OwnerFirstName.Length != 0)
            {
                entity.OwnerFirstName = model.OwnerFirstName;
            }
            if (model.OwnerlastName != null && model.OwnerlastName.Length != 0)
            {
                entity.OwnerlastName = model.OwnerlastName;
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

        public override RanchModel CreateModelForIndividualLookup(Ranch entity)
        {
            return new RanchModel
            {
                Id = entity.Id,
                OwnerFirstName = entity.OwnerFirstName,
                OwnerlastName = entity.OwnerlastName,
                Email = entity.Email,
                Status = entity.Status,
                City = entity.City,
                State = entity.State,
                LoginDataId = entity.LoginDataId,
                CreateDate = entity.CreateDate,
                UpdateDate = entity.UpdateDate
            };
        }

        public override RanchModel CreateModelForListLookup(Ranch entity)
        {
            return new RanchModel
            {
                Status = entity.Status,
                Email = entity.Email,
                LoginDataId = entity.LoginDataId,
                OwnerFirstName = entity.OwnerFirstName,
                OwnerlastName = entity.OwnerlastName,
                City = entity.City,
                State = entity.State,
                CreateDate = entity.CreateDate,
                UpdateDate = entity.UpdateDate
            };
        }

        public Ranch GetByIDNoTracking(Guid id)
        {
            return _ranchRepo.GetByNoTrackingId(id);
        }
        public Ranch Login(string encodedAuthRequest)
        {
            string[] parsedAuthRequest = _authHelper.decodeAuth(encodedAuthRequest);
            string ranchname = parsedAuthRequest[0];
            string password = parsedAuthRequest[1];

            if (string.IsNullOrEmpty(ranchname) || string.IsNullOrEmpty(password))
            {
                throw new Exception("TODO: ERROR");
            }

            Ranch ranch = _ranchRepo.GetAuthRanch(ranchname);

            if (ranch == null)
            {
                throw new Exception("TODO: ERROR");
            }

            if (!_authHelper.CheckCredentials(ranchname, password, UserLoginTypes.Ranch))
            {
                throw new Exception("TODO: ERROR");
            }

            return ranch;
        }

        public string GenerateToken(Ranch ranch)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, ranch.Id.ToString()),
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
            ChangePassword changePassword = _ranchRepo.GetChangePasswordRequest(changePasswordId);
            Ranch ranch = changePassword.Ranch;

            if (changePassword == null || DateTime.Now.CompareTo(changePassword.ExpirationDate) > 0)
            {
                return false;
            }

            if (ranch == null)
            {
                return false;
            }

            byte[] newSalt = _authHelper.GenerateSalt();
            ranch.LoginData!.Password = _authHelper.Sha256(password, newSalt);
            ranch.LoginData!.Salt = Convert.ToBase64String(newSalt);

            _ranchRepo.SaveChanges();

            return true;
        }

        //TODO: Once we get front end set up and figure out urls finish setting this up
        public bool ForgotPassword(string email)
        {
            Ranch ranch = _ranchRepo.GetRanchByEmail(email);

            if (ranch == null)
            {
                return false;
            }

            ChangePasswordModel changePasswordModel = new ChangePasswordModel();
            changePasswordModel.RanchId = ranch.Id;

            string changePasswordId = _changePasswordService.Create(changePasswordModel).ToString();

            if (changePasswordId == null)
            {
                return false;
            }

            //string currentHost = _healthPossibleDbContext.Settings.FirstOrDefault(x => x.SettingKey == "CUSTOMER_URL").SettingValue;
            string currentHost = "https://localhost:7145";
            string Url = "";

            if (currentHost.Contains("localhost"))
            {
                Url = $"http://{currentHost}/{string.Concat("change-password/", changePasswordId.ToString())}";
            }
            //TODO: SET UP FOR CLOUD DB
            //else
            //{
            //    UriBuilder changePasswordUriBuilder = new UriBuilder()
            //    {
            //        Scheme = "https",
            //        Host = _healthPossibleDbContext.Settings.FirstOrDefault(x => x.SettingKey == "CUSTOMER_URL")
            //                .SettingValue,
            //        Path = string.Concat("change-password/", changePassword.ChangePasswordId.ToString())
            //    };

            //    Url = changePasswordUriBuilder.ToString();
            //}


            bool emailStatus = _emailService.SendEmail(
                ranch.Email,
                EmailConstants.ChangePasswordSubject,
                string.Format(EmailConstants.ChangePasswordBody, ranch.Name, Url),
                null
            );

            return emailStatus;
        }


        public override BaseRepo<Ranch> LoadRepo()
        {
            return _ranchRepo;
        }
    }
}
