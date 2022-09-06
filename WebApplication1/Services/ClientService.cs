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
    public class ClientService : BaseService<ClientModel, Client>
    {
        private readonly ClientRepo _clientRepo;
        private readonly LoginDataService _loginDataService;
        private readonly SubDataService _subDataService;
        private readonly IConfiguration _configuration;
        private readonly AuthHelper _authHelper;
        private readonly ChangePasswordService _changePasswordService;
        private readonly EmailService _emailService;
        public ClientService(ClientRepo clientRepo, 
                            LoginDataService loginDataService, 
                            SubDataService subDataService, 
                            IConfiguration configuration, 
                            AuthHelper authHelper,
                            ChangePasswordService changerPasswordService,
                            EmailService emailService)
        {
            _clientRepo = clientRepo;
            _loginDataService = loginDataService;
            _subDataService = subDataService;
            _configuration = configuration;
            _authHelper = authHelper;
            _changePasswordService = changerPasswordService;
            _emailService = emailService;
        }

        //TODO: Figure out a way if there is any fail delete everything that happened!
        protected override void AdditionalPreAddLogic(Client entity)
        {
            Client? client = _clientRepo.CheckIfEmailExists(entity.Email);

            if(client == null)
            {
                LoginDataModel loginData = new LoginDataModel();
                entity.LoginDataId = _loginDataService.Create(loginData);

                SubDataModel subData = new SubDataModel();
                entity.SubDataId = _subDataService.Create(subData);
            }
            else
            {
                throw new Exception("Failed to add Client");
            }
        }

        protected override void AdditonalPostAddLogic(Client entity)
        {
            var emailSuccess = ForgotPassword(entity.Email);
            if (!emailSuccess)
            {
                throw new Exception("Email failed to send.");
            }
        }

        public override Client ConverToEntityForAdd(ClientModel model)
        {
            return new Client
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

        public override void CopyDataForUpdate(Client entity, ClientModel model)
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

        public override ClientModel CreateModelForIndividualLookup(Client entity)
        {
            return new ClientModel
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

        public override ClientModel CreateModelForListLookup(Client entity)
        {
            return new ClientModel
            {
                Status = entity.Status,
                Email = entity.Email,
                LoginDataId = entity.LoginDataId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                City = entity.City,
                State = entity.State,
                SubDataId = entity.SubDataId,
                CreateDate = entity.CreateDate,
                UpdateDate = entity.UpdateDate
            };
        }

        public Client GetByIDNoTracking(Guid id)
        {
            return _clientRepo.GetByNoTrackingId(id);
        }
        public Client Login(string encodedAuthRequest)
        {
            string[] parsedAuthRequest = _authHelper.decodeAuth(encodedAuthRequest);
            string username = parsedAuthRequest[0];
            string password = parsedAuthRequest[1];

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new Exception("TODO: ERROR");
            }

            Client client = _clientRepo.GetAuthClient(username);

            if (client == null)
            {
                throw new Exception("TODO: ERROR");
            }

            if (!_authHelper.CheckCredentials(username, password, UserLoginTypes.Client))
            {
                throw new Exception("TODO: ERROR");
            }

            return client;
        }

            public string GenerateToken(Client client)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, client.Id.ToString()),
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
            ChangePassword changePassword = _clientRepo.GetChangePasswordRequest(changePasswordId);
            Client client = changePassword.Client;

            if (changePassword == null || DateTime.Now.CompareTo(changePassword.ExpirationDate) > 0)
            {
                return false;
            }

            if (client == null)
            {
                return false;
            }

            byte[] newSalt = _authHelper.GenerateSalt();
            client.LoginData!.Password = _authHelper.Sha256(password, newSalt);
            client.LoginData!.Salt = Convert.ToBase64String(newSalt);

            _clientRepo.SaveChanges();

            return true;
        }

        //TODO: Once we get front end set up and figure out urls finish setting this up
        public bool ForgotPassword(string email)
        {
            Client client = _clientRepo.GetClientByEmail(email);

            if (client == null)
            {
                return false;
            }

            ChangePasswordModel changePasswordModel = new ChangePasswordModel();
            changePasswordModel.ClientId = client.Id;

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
                client.Email,
                EmailConstants.ChangePasswordSubject,
                string.Format(EmailConstants.ChangePasswordBody, client.FirstName, Url),
                null
            );

            return emailStatus;
        }


        public override BaseRepo<Client> LoadRepo()
        {
            return _clientRepo;
        }
    }
}
