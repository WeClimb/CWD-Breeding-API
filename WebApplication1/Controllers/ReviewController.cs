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
    public class ReviewController : BaseController<ReviewModel, Review>
    {
        private readonly ReviewService _reviewService;

        public ReviewController(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        public override BaseService<ReviewModel, Review> LoadService()
        {
            return _reviewService;
        }

        [HttpPost]
        public IActionResult CreateReview(ReviewModel model)
        {
            return Create(model);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            return LoadById(id);
        }

        [HttpPut("{id:Guid}")]
        public IActionResult UpdateReview(Guid id, ReviewModel model)
        {
            return Update(id, model);
        }
    }
}
