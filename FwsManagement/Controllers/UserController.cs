using Fws.Model.Models;
using Fws.Service.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FwsManagement.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        protected IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public IActionResult GetAllUser()
        {
            var result = _service.GetAllUser();
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult AuthenTiCation([FromBody] UserLoginModel user)
        {
            try
            {
                var Userlogin = _service.CheckAuthencation(user.UserName, user.PassWord);

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

        /// <summary>
        /// sinh ra token mới khi token cũ hết hạn
        /// </summary>
        /// <param name="authen"> chứa token và refresh token</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("generate-token")]
        public IActionResult GenerateToken([FromBody] TokenResultModel listToken)
        {
            if (ModelState.IsValid)
            {
                UserCommon result = new UserCommon();
                // validate 2 token
                var validate = _service.ValidateToken(listToken);
                if (validate.Status)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }

            }
            return BadRequest();
        }
    }
}
