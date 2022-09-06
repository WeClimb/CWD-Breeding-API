using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReviewPlatformAPI.Entities;
using ReviewPlatformAPI.Models;
using ReviewPlatformAPI.Models.Non_EntityModels;
using ReviewPlatformAPI.Services;
using ReviewPlatformAPI.Utils;

namespace ReviewPlatformAPI.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ClientController : BaseController<ClientModel, Client>
    {
        private readonly ClientService _clientService;

        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
        }
        public override BaseService<ClientModel, Client> LoadService()
        {
            return _clientService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public IActionResult Login()
        {
            string encodedAuthRequest = Request.Headers["Authorization"];
            Client client = _clientService.Login(encodedAuthRequest);

            if (client == null)
            {
                return BadRequest(new { message = "Invalid credentials" });
            }

            string tokenString = _clientService.GenerateToken(client);

            // return basic user info and authentication token
            return Ok(new
            {
                Id = client.Id,
                Username = client.Email,
                FirstName = client.FirstName,
                LastName = client.LastName,
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
                _clientService.ChangePassword(new Guid(passwordRequestModel.changePasswordId), passwordRequestModel.password);
                return Ok(new { message = "Success!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateClient(ClientModel model)
        {
            return Create(model);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            return LoadById(id);
        }

        [HttpPut("{id:Guid}")]
        public IActionResult UpdateClient(Guid id, ClientModel model)
        {
            return Update(id, model);
        }
    }
}
