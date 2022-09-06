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
    public class ClientReviewController : BaseController<ClientReviewModel, ClientReview>
    {
        private readonly ClientReviewService _clientReviewService;

        public ClientReviewController(ClientReviewService clientReviewService)
        {
            _clientReviewService = clientReviewService;
        }
        public override BaseService<ClientReviewModel, ClientReview> LoadService()
        {
            return _clientReviewService;
        }

        [HttpPost]
        public IActionResult CreateClientReview(ClientReviewModel model)
        {
            return Create(model);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            return LoadById(id);
        }

        [HttpPut("{id:Guid}")]
        public IActionResult UpdateClientReview(Guid id, ClientReviewModel model)
        {
            return Update(id, model);
        }
    }
}
