using Microsoft.AspNetCore.Mvc;
using ReviewPlatformAPI.Models;
using ReviewPlatformAPI.Services;
using ServiceProvider = ReviewPlatformAPI.Entities.ServiceProvider;
using Microsoft.AspNetCore.Authorization;
using ReviewPlatformAPI.Models.Non_EntityModels;

namespace ReviewPlatformAPI.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ServiceProviderController : BaseController<ServiceProviderModel, ServiceProvider>
    {
        private readonly ServiceProviderService _serviceProviderService;

        public ServiceProviderController(ServiceProviderService serviceProviderService)
        {
            _serviceProviderService = serviceProviderService;
        }
        public override BaseService<ServiceProviderModel, ServiceProvider> LoadService()
        {
            return _serviceProviderService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public IActionResult Login()
        {
            string encodedAuthRequest = Request.Headers["Authorization"];
            ServiceProvider serviceProvider = _serviceProviderService.Login(encodedAuthRequest);

            if (serviceProvider == null)
            {
                return BadRequest(new { message = "Invalid credentials" });
            }

            string tokenString = _serviceProviderService.GenerateToken(serviceProvider);

            // return basic user info and authentication token
            return Ok(new
            {
                Id = serviceProvider.Id,
                Username = serviceProvider.Email,
                FirstName = serviceProvider.FirstName,
                LastName = serviceProvider.LastName,
                Token = tokenString,
            });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ChangePassword")]
        public IActionResult ChangePassword(PasswordRequestModel passwordRequestModel)
        {
            //TODO:Add a better password check here, ex: length, symbols, numbers
            if (string.IsNullOrEmpty(passwordRequestModel.password))
            {
                throw new Exception("Password is required.");
            }

            try
            {
                _serviceProviderService.ChangePassword(new Guid(passwordRequestModel.changePasswordId), passwordRequestModel.password);
                return Ok(new { message = "Success!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateServiceProvider(ServiceProviderModel model)
        {
            return Create(model);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            return LoadById(id);
        }

        [HttpPut("{id:Guid}")]
        public IActionResult UpdateServiceProvider(Guid id, ServiceProviderModel model)
        {
            return Update(id, model);
        }
    }
}
