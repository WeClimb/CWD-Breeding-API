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
    public class ServiceProviderReviewController : BaseController<ServiceProviderReviewModel, ServiceProviderReview>
    {
        private readonly ServiceProviderReviewService _serviceProviderReviewService;

        public ServiceProviderReviewController(ServiceProviderReviewService serviceProviderReviewService)
        {
            _serviceProviderReviewService = serviceProviderReviewService;
        }
        public override BaseService<ServiceProviderReviewModel, ServiceProviderReview> LoadService()
        {
            return _serviceProviderReviewService;
        }

        [HttpPost]
        public IActionResult CreateServiceProviderReview(ServiceProviderReviewModel model)
        {
            return Create(model);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            return LoadById(id);
        }

        [HttpPut("{id:Guid}")]
        public IActionResult UpdateServiceProviderReview(Guid id, ServiceProviderReviewModel model)
        {
            return Update(id, model);
        }
    }
}
