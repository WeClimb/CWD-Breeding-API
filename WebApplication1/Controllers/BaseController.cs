using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using ReviewPlatformAPI.Entities;
using ReviewPlatformAPI.Models;
using ReviewPlatformAPI.Services;

namespace ReviewPlatformAPI.Controllers
{
    [ApiController]
    public abstract class BaseController<Model, Entity> : ControllerBase where Model : BaseModel where Entity : BaseEntity
    {
        public abstract BaseService<Model, Entity> LoadService();

        public IActionResult Create(Model model)
        {
            try
            {
                Guid id = LoadService().Create(model);

                var url = Response.HttpContext.Request.GetEncodedUrl() + "/" + id;

                return new CreatedResult(url, new { Id = id });
            } 
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [NonAction]
        public IActionResult LoadById(Guid id)
        {
            return Ok(LoadService().LookupById(id));
        }

        [NonAction]
        public IActionResult Update(Guid id, Model model)
        {
            try
            {
                LoadService().Update(id, model);

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }   
        }
    }
}
