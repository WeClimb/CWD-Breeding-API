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
    public class ReviewChangeRequestController : BaseController<ReviewChangeRequestModel, ReviewChangeRequest>
    {
        private readonly ReviewChangeRequestService _reviewChangeRequestService;

        public ReviewChangeRequestController(ReviewChangeRequestService reviewChangeRequestService)
        {
            _reviewChangeRequestService = reviewChangeRequestService;
        }
        public override BaseService<ReviewChangeRequestModel, ReviewChangeRequest> LoadService()
        {
            return _reviewChangeRequestService;
        }

        [HttpPost]
        public IActionResult CreateReviewChangeRequest(ReviewChangeRequestModel model)
        {
            return Create(model);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            return LoadById(id);
        }

        [HttpPut("{id:Guid}")]
        public IActionResult UpdateReviewChangeRequest(Guid id, ReviewChangeRequestModel model)
        {
            return Update(id, model);
        }
    }
}
