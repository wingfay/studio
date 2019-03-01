using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServiceA.Controllers
{
   [Authorize] //添加 Authorize Attribute 以使该控制器启用认证
   [Route("api/[controller]")]
   [ApiController]
   public class ValuesController : ControllerBase
   {
      // GET api/values
      [HttpGet]
      public ActionResult<IEnumerable<string>> Get()
      {
         return new[] { "value1", "value2" };
      }
   }
}
