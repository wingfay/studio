using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceExample.Models
{
   public class Merchant
   {

      public Merchant(string storeId,string apiToken, EnvironmentType environment, ProcessingCountryType processingCountry)
      {
         StoreId = storeId;
         ApiToken = apiToken;
         Environment = environment;
         ProcessingCountry = processingCountry;
      }

      public enum EnvironmentType
      {

         QA,
         Prod
      }

      public enum ProcessingCountryType
      {

         CA,
         US
      }


      #region Environment


      public EnvironmentType Environment { get; set; }

      public void TestMode()
      {
         Environment = EnvironmentType.QA;
      }

      public void ProductMode()
      {
         Environment = EnvironmentType.Prod;
      }

      #endregion


      public string StoreId { get; private set; }

      public string ApiToken { get; private set; }

      public ProcessingCountryType ProcessingCountry { get; private set; }


   }


}
