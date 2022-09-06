using Microsoft.IdentityModel.Tokens;
using ReviewPlatformAPI.Constants;
using ReviewPlatformAPI.Entities;
using ReviewPlatformAPI.Models;
using ReviewPlatformAPI.Repos;
using ReviewPlatformAPI.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ServiceProvider = ReviewPlatformAPI.Entities.ServiceProvider;

namespace ReviewPlatformAPI.Services
{
    public class ServiceProviderService : BaseService<ServiceProviderModel, ServiceProvider>
    {
        private readonly ServiceProviderRepo _serviceProviderRepo;
        private readonly LoginDataService _loginDataService;
        private readonly SubDataService _subDataService;
        private readonly IConfiguration _configuration;
        private readonly AuthHelper _authHelper;
        private readonly ChangePasswordService _changePasswordService;
        private readonly EmailService _emailService;

        public ServiceProviderService(ServiceProviderRepo serviceProviderRepo, 
                                      LoginDataService loginDataService, 
                                      SubDataService subDataService,
                                      IConfiguration configuration,
                                      AuthHelper authHelper,
                                      ChangePasswordService changerPasswordService,
                                      EmailService emailService)
        {
            _serviceProviderRepo = serviceProviderRepo;
            _loginDataService = loginDataService;
            _subDataService = subDataService;
            _configuration = configuration;
            _authHelper = authHelper;
            _changePasswordService = changerPasswordService;
            _emailService = emailService;
        }
        //TODO: Figure out a way if there is any fail delete everything that happened!
        protected override void AdditionalPreAddLogic(ServiceProvider entity)
        {
            ServiceProvider? serviceProvider = _serviceProviderRepo.CheckIfEmailExists(entity.Email);

            if (serviceProvider == null)
            {
                LoginDataModel loginData = new LoginDataModel();
                entity.LoginDataId = _loginDataService.Create(loginData);

                SubDataModel subData = new SubDataModel();
                entity.SubDataId = _subDataService.Create(subData);
            } 
            else
            {
                //TODO: Make this more clear
                throw new Exception("Failed to add Service Provider");
            }
        }
        protected override void AdditonalPostAddLogic(ServiceProvider entity)
        {
            var emailSuccess = ForgotPassword(entity.Email);
            if (!emailSuccess)
            {
                throw new Exception("Email failed to send.");
            }
        }
        public override ServiceProvider ConverToEntityForAdd(ServiceProviderModel model)
        {
            return new ServiceProvider
            {
                Status = "ACTIVE",
                Email = model.Email,
                FirstName = model.FirstName,
                City = model.City,
                State = model.State,
                LastName = model.LastName,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
            };
        }

        public override void CopyDataForUpdate(ServiceProvider entity, ServiceProviderModel model)
        {
            if (model.FirstName != null && model.FirstName.Length != 0)
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
            if (model.State != null && model.State.Length != 0)
            {
                entity.State = model.State;
            }
            if (model.City != null && model.City.Length != 0)
            {
                entity.City = model.City;
            }

            entity.Status = model.Status ?? entity.Status;
            entity.UpdateDate = DateTime.Now;
        }

        public override ServiceProviderModel CreateModelForIndividualLookup(ServiceProvider entity)
        {
            return new ServiceProviderModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Status = entity.Status,
                City = entity.City,
                State = entity.State,
                LoginDataId = entity.LoginDataId,
                SubDataId = entity.SubDataId,
                CreateDate = entity.CreateDate,
                UpdateDate = entity.UpdateDate
            };
        }

        public override ServiceProviderModel CreateModelForListLookup(ServiceProvider entity)
        {
            return new ServiceProviderModel
            {
                Status = entity.Status,
                Email = entity.Email,
                LoginDataId = entity.LoginDataId,
                FirstName = entity.FirstName,
                City = entity.City,
                State = entity.State,
                LastName = entity.LastName,
                SubDataId = entity.SubDataId,
                CreateDate = entity.CreateDate,
                UpdateDate = entity.UpdateDate
            };
        }

        public ServiceProvider GetByIDNoTracking(Guid id)
        {
            return _serviceProviderRepo.GetByNoTrackingId(id);
        }
        public ServiceProvider Login(string encodedAuthRequest)
        {
            string[] parsedAuthRequest = _authHelper.decodeAuth(encodedAuthRequest);
            string username = parsedAuthRequest[0];
            string password = parsedAuthRequest[1];

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new Exception("TODO: ERROR");
            }

            ServiceProvider serviceProvider = _serviceProviderRepo.GetAuthClient(username);

            if (serviceProvider == null)
            {
                throw new Exception("TODO: ERROR");
            }

            if (!_authHelper.CheckCredentials(username, password, UserLoginTypes.ServiceProvider))
            {
                throw new Exception("TODO: ERROR");
            }

            return serviceProvider;
        }

        public string GenerateToken(ServiceProvider serviceProvider)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, serviceProvider.Id.ToString()),
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
            ChangePassword changePassword = _serviceProviderRepo.GetChangePasswordRequest(changePasswordId);
            ServiceProvider serviceProvider = changePassword.ServiceProvider;

            if (changePassword == null || DateTime.Now.CompareTo(changePassword.ExpirationDate) > 0)
            {
                return false;
            }

            if (serviceProvider == null)
            {
                return false;
            }

            byte[] newSalt = _authHelper.GenerateSalt();
            serviceProvider.LoginData!.Password = _authHelper.Sha256(password, newSalt);
            serviceProvider.LoginData!.Salt = Convert.ToBase64String(newSalt);

            _serviceProviderRepo.SaveChanges();

            return true;
        }

        //TODO: Once we get front end set up and figure out urls finish setting this up
        public bool ForgotPassword(string email)
        {
            ServiceProvider serviceProvider = _serviceProviderRepo.GetServiceProviderByEmail(email);

            if (serviceProvider == null)
            {
                return false;
            }

            ChangePasswordModel changePasswordModel = new ChangePasswordModel();
            changePasswordModel.ServiceProviderId = serviceProvider.Id;

            string changePasswordId = _changePasswordService.Create(changePasswordModel).ToString();

            if (changePasswordId == null)
            {
                return false;
            }

            //string currentHost = _healthPossibleDbContext.Settings.FirstOrDefault(x => x.SettingKey == "CUSTOMER_URL").SettingValue;
            string currentHost = "https://localhost:4200";
            string Url = "";

            if (currentHost.Contains("localhost"))
            {
                Url = $"http://{currentHost}/{string.Concat("change-password/", changePasswordId.ToString())}";
            }
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
                serviceProvider.Email,
                EmailConstants.ChangePasswordSubject,
                string.Format(EmailConstants.ChangePasswordBody, serviceProvider.FirstName, Url),
                null
            );

            return emailStatus;
        }


        public override BaseRepo<ServiceProvider> LoadRepo()
        {
            return _serviceProviderRepo;
        }
    }
}
