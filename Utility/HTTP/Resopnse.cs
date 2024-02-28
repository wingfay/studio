using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Utility.HTTP
{
   public class Resopnse : IResopnse
   {
      public HttpStatusCode StatusCode { get; set; }
      public string Result { get; set; }
   }
}
