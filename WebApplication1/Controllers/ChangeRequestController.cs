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
    public class ChangeRequestController : BaseController<ChangeRequestModel, ChangeRequest>
    {
        private readonly ChangeRequestService _changeRequestService;

        public ChangeRequestController(ChangeRequestService changeRequestService)
        {
            _changeRequestService = changeRequestService;
        }
        public override BaseService<ChangeRequestModel, ChangeRequest> LoadService()
        {
            return _changeRequestService;
        }

        [HttpPost]
        public IActionResult CreateChangeRequest(ChangeRequestModel model)
        {
            return Create(model);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            return LoadById(id);
        }

        [HttpPut("{id:Guid}")]
        public IActionResult UpdateChangeRequest(Guid id, ChangeRequestModel model)
        {
            return Update(id, model);
        }
    }
}
