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
    public class DeerController : BaseController<DeerModel, Deer>
    {
        private readonly DeerService _deerService;

        public DeerController(DeerService deerService)
        {
            _deerService = deerService;
        }
        public override BaseService<DeerModel, Deer> LoadService()
        {
            return _deerService;
        }

        [HttpPost]
        public IActionResult CreateDeer(DeerModel model)
        {
            return Create(model);
        }

        [HttpPost]
        [Route("Request-Listing")]
        public IActionResult RequestDeerListing([FromBody]DeerModel model)
        {
            try
            {
                string id = _deerService.CreateDeerRequest(model);
                return Ok(new { id});
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            DeerModel? model = _deerService.GetById(id);
            if(model != null)
            {
                return Ok(model);
            } 
            else
            {
                return BadRequest("Request Failed");
            }
        }

        [HttpPut("{id:Guid}")]
        public IActionResult UpdateDeer(Guid id, DeerModel model)
        {
            return Update(id, model);
        }

        [Route("Denied")]
        [HttpPut]
        public IActionResult DenyDeerRequest(DeerModel model)
            {
            try
            {
                _deerService.DenyRequest(model);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("All")]
        public List<DeerModel> All(bool isApproved = false)
        {
            return _deerService.GetAll(isApproved);
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("All-Filtered")]
        public List<DeerModel> GetAllFiltered(string? deerName = "", string? ranchName = "", string? codon = "", decimal? gebv = null, int? age = null, int? sciScore = null, bool isApproved = false)
        {
            return _deerService.GetAllFiltered(isApproved, deerName, ranchName, codon, gebv, age, sciScore);
        }


        [AllowAnonymous]
        [HttpGet("{id:guid}/ProfileImage")]
        public IActionResult GetProfileImage(Guid id)
        {
            return Ok(new { data = _deerService.GetProfileImageBytes(id) });
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}/Images")]
        public IActionResult GetImages(Guid id)
        {
            return Ok(new { data = _deerService.GetImageBytes(id) });
        }

        [HttpPost("{id:guid}/ProfileImage")]
        public IActionResult SaveProfileImage(Guid id, [FromForm] IFormFile profileImg)
        {
            try
            {
                if (_deerService.SaveProfileImage(id, profileImg))
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Image did not save");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id:guid}/Extra-Image")]
        public IActionResult SaveImage(Guid id, [FromForm] IFormFile image)
        {
            try
            {
                if (_deerService.SaveImage(id, image))
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Image did not save");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
