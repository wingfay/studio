using System.Net;
using System.Xml;

namespace Utility.HTTP
{
   public class ResopnseXML
   {
      public HttpStatusCode StatusCode { get; set; }
      public XmlDocument Result { get; set; }
   }
}
