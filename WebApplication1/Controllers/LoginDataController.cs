using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReviewPlatformAPI.Entities;
using ReviewPlatformAPI.Models;
using ReviewPlatformAPI.Services;

namespace ReviewPlatformAPI.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class LoginDataController : BaseController<LoginDataModel, LoginData>
    {
        private readonly LoginDataService _loginDataService;

        public LoginDataController(LoginDataService loginDataService)
        {
            _loginDataService = loginDataService;
        }
        public override BaseService<LoginDataModel, LoginData> LoadService()
        {
            return _loginDataService;
        }

        [HttpPost]
        public IActionResult CreateLoginData(LoginDataModel model)
        {
            return Create(model);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            return LoadById(id);
        }

        [HttpPut("{id:Guid}")]
        public IActionResult UpdateLoginData(Guid id, LoginDataModel model)
        {
            return Update(id, model);
        }
    }
}
