using Fws.Model.Entities;
using Fws.Model.Models;
using Fws.Service.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace FwsManagement.Controllers
{
    
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        protected IUserService _service;
        protected IConfiguration _config;
        public UserController(IUserService service, IConfiguration config)
        {
            _service = service;
            _config = config;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllUser()
        {
            var result = _service.GetAllUser();
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult AuthenTiCation([FromBody] AuthenticateModel authen)
        {
            try
            {
                var keyHash = _config.GetSection("JwtConfig").GetSection("key").Value;
                var Userlogin = _service.CheckAuthencation(authen.UserName, authen.PassWord, keyHash);

                if (Userlogin.Status)
                {
                    return Ok(Userlogin);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
    }
}
