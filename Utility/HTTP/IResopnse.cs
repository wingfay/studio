using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Utility.HTTP
{
   public interface IResopnse
   {
      HttpStatusCode StatusCode { get; set; }
      string Result { get; set; }


   }

}
