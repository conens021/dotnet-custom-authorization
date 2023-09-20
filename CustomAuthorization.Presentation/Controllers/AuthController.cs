using CustomAuthorization.BLL.DTOs;
using CustomAuthorization.Presentation.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomAuthorization.Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private JwtAuthenticationManager _jwt;


        public AuthController(JwtAuthenticationManager jwt)
        {
            _jwt = jwt;
        }

        [AllowAnonymous]
        [HttpPost("admin")]
        public ActionResult Auth(string username,string password)
        {

            AuthUser authUser = new AuthUser()
            {   Id = 1,
                Username = username,
                Email = username,
                Role = BLL.Enums.UserRole.SuperAdmin 
            };

            return Ok(_jwt.Authenticate(authUser));
        }

        [AllowAnonymous]
        [HttpPost("creator")]
        public ActionResult AuthCreator(string username, string password)
        {

            AuthUser authUser = new AuthUser()
            {
                Id = 1,
                Username = username,
                Email = username,
                Role = BLL.Enums.UserRole.Creator
            };

            return Ok(_jwt.Authenticate(authUser));
        }
    }
}
