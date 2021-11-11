using Fws.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FwsManagement.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        public readonly IBaseService<T> _baseService;

        public BaseController(IBaseService<T> baseService) 
        {
            _baseService = baseService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _baseService.GetAll();
            return Ok(result);
        }

    }
}
