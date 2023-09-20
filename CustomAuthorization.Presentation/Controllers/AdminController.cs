using CustomAuthorization.BLL.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomAuthorization.Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [Authorize(Policy  = "Admin")]
        [HttpGet("/admin")]
        public ActionResult Get()
        {
            return Ok("moze admin si!");
        }

        [Authorize(Policy = "Creator")]
        [HttpGet("/creator")]
        public ActionResult GetCreateor()
        {
            return Ok("moze kreator si!");
        }
    }
}
