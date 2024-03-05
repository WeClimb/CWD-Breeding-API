using CWDBreedingAPI.Entities;
using CWDBreedingAPI.Models.Non_EntityModels;
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

        [HttpPost]
        [Route("Send-Email")]
        public IActionResult SendListingRequestEmail([FromBody] DeerModel[] model)
        {
            try
            {
                _deerService.SendAddDeerEmail(model);
                return Ok();
            }
            catch (Exception ex)
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

        [HttpPut]
        [Route("{deerId}/Pedigree")]
        public IActionResult UpdatePedigree(Guid deerId, [FromBody] DeerFamilyModel deerFamily)
        {
            bool request = _deerService.UpdateDeerFamily(deerId, deerFamily);

            if(request)
            {
                return Ok();
            } 
            else
            {
                return BadRequest();
            }
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


        [Route("Approve")]
        [HttpPut]
        public IActionResult ApproveDeerRequest(DeerModel model)
        {
            try
            {
                _deerService.ApproveRequest(model);
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
        public List<DeerModel> All(bool isApproved = false, bool isPaid = true)
        {
            return _deerService.GetAll(isApproved, isPaid);
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("All-Filtered")]
        public List<DeerModel> GetAllFiltered(int? page = 0, string? deerName = "", string? ranchName = "", string? codon = "", decimal? gebv = null, int? age = null, int? sciScore = null, string? ranchId = "" ,bool isApproved = false)
        {
            return _deerService.GetAllFiltered(isApproved, deerName, ranchName, codon, gebv, age, sciScore, page, ranchId);
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
        public IActionResult SaveProfileImage(Guid id, [FromForm] IFormFile profileImg, int? age)
        {
            try
            {
                if (_deerService.SaveProfileImage(id, profileImg, age))
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
        public IActionResult SaveImage(Guid id, [FromForm] IFormFile image, int? age)
        {
            try
            {
                if (_deerService.SaveImage(id, image, age))
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

        //[HttpPost("Create-Subscriptions")]
        //public async Task<IActionResult> CreateSubscriptions([FromBody] List<DeerSubsciptionModel> requests)
        //{
        //    var checkoutSession = await _deerService.CreateSubscriptions(requests);

        //    return Ok(checkoutSession);
        //}


    }
}
