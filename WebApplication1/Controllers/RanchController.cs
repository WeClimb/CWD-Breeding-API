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
    public class RanchController : BaseController<RanchModel, Ranch>
    {
        private readonly RanchService _ranchService;

        public RanchController(RanchService ranchService)
        {
            _ranchService = ranchService;
        }
        public override BaseService<RanchModel, Ranch> LoadService()
        {
            return _ranchService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public IActionResult Login()
        {
            try
            {
                string encodedAuthRequest = Request.Headers["Authorization"];
                Ranch ranch = _ranchService.Login(encodedAuthRequest);

                if (ranch == null)
                {
                    return BadRequest(new { message = "Invalid credentials" });
                }

                string tokenString = _ranchService.GenerateToken(ranch);

                // return basic ranch info and authentication token
                return Ok(new
                {
                    Id = ranch.Id,
                    Email = ranch.Email,
                    Ranch = ranch.Name,
                    OwnerFirstName = ranch.OwnerFirstName,
                    OwnerlastName = ranch.OwnerlastName,
                    LoginType = Constants.UserLoginTypes.Ranch,
                    Token = tokenString,
                });
            } 
            catch(Exception x)
            {
                return BadRequest(new { message = "Authentication Failed" });
            }
          
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
                _ranchService.ChangePassword(new Guid(passwordRequestModel.changePasswordId), passwordRequestModel.password);
                return Ok(new { message = "Success!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateRanch(RanchModel model)
        {
            return Create(model);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            return LoadById(id);
        }

        [HttpPut("{id:Guid}")]
        public IActionResult UpdateRanch(Guid id, RanchModel model)
        {
            return Update(id, model);
        }

        //[HttpGet]
        //[Route("All")]
        //public List<RanchModel> All(string? firstName, string? lastName, string? city, string? state)
        //{
        //    return _ranchService.GetA(firstName,lastName,city,state);
        //}
    }
}
