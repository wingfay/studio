using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp
{
   public class ResponseModel
   {
      public XmlDocument XmlDoc { get; set; }

      public HttpStatusCode StatusCode { get; set; }
   }

}
