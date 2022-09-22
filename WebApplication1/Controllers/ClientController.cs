using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReviewPlatformAPI.Entities;
using ReviewPlatformAPI.Models;
using ReviewPlatformAPI.Models.Non_EntityModels;
using ReviewPlatformAPI.Services;

namespace ReviewPlatformAPI.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : BaseController<UserModel, User>
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }
        public override BaseService<UserModel, User> LoadService()
        {
            return _userService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public IActionResult Login()
        {
            string encodedAuthRequest = Request.Headers["Authorization"];
            User user = _userService.Login(encodedAuthRequest);

            if (user == null)
            {
                return BadRequest(new { message = "Invalid credentials" });
            }

            string tokenString = _userService.GenerateToken(user);

            // return basic user info and authentication token
            return Ok(new
            {
                Id = user.Id,
                Username = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                LoginType = user.LoginData.LoginType,
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
                _userService.ChangePassword(new Guid(passwordRequestModel.changePasswordId), passwordRequestModel.password);
                return Ok(new { message = "Success!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateUser(UserModel model)
        {
            return Create(model);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            return LoadById(id);
        }

        [HttpPut("{id:Guid}")]
        public IActionResult UpdateUser(Guid id, UserModel model)
        {
            return Update(id, model);
        }

        //[HttpGet]
        //[Route("All")]
        //public List<UserModel> All(string? firstName, string? lastName, string? city, string? state)
        //{
        //    return _userService.GetA(firstName,lastName,city,state);
        //}
    }
}
