using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServiceB.Controllers
{
   [Route("api/[controller]"),ApiController]
   public class HealthController : ControllerBase
   {
      /// <summary>
      /// 健康检查
      /// </summary>
      /// <returns></returns>
      [HttpGet]
      public IActionResult Get()
      {
         return Ok();
      }
   }
}
