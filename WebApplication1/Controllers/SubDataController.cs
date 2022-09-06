using Microsoft.AspNetCore.Mvc;
using ReviewPlatformAPI.Entities;
using ReviewPlatformAPI.Models;
using ReviewPlatformAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace ReviewPlatformAPI.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class SubDataController : BaseController<SubDataModel, SubData>
    {
        private readonly SubDataService _subDataService;

        public SubDataController(SubDataService subDataService)
        {
            _subDataService = subDataService;
        }
        public override BaseService<SubDataModel, SubData> LoadService()
        {
            return _subDataService;
        }

        [HttpPost]
        public IActionResult CreateSubData(SubDataModel model)
        {
            return Create(model);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            return LoadById(id);
        }

        [HttpPut("{id:Guid}")]
        public IActionResult UpdateSubData(Guid id, SubDataModel model)
        {
            return Update(id, model);
        }
    }
}
