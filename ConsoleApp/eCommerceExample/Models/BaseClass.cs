using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceExample.Models
{
   public class BaseClass
   {

      internal string _storeID { get; set; }
      internal string _aPIToken { get; set; }
      public string ProcessingCountry { get; set; }
      public string Environment { get; set; }


      public BaseClass()
      {
         ProcessingCountry = "CA";
      }


      internal void SetProdcutionEnvironment()
      {
         _storeID = "moneris";
         _aPIToken = "EB6I4LlAHrSy2Y50oSXH";
         Environment = "Prod";
      }

      internal void SetQAEnvironment()
      {
         _storeID = "monca00597";
         _aPIToken = "hxgbcbBXNEASN6wiGLFP";
         Environment = "QA";
      }
   }
}
