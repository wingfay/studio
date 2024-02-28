using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossRef
{
	class Program
	{
		static void Main(string[] args)
		{
			string url = "https://api.crossref.org/works?query.title=programming";




		}


      public static void TestUnPaywall()
      {

         IHttpRequest httpRequest = new HttpRequest();
         //string strValue = "10.1038/nature12373";
         string strValue = "test";

         string searchURL = "https://api.unpaywall.org/v2/{0}?email=YOUR_EMAIL";
         try
         {
            IResopnse resopnse = httpRequest.Get(string.Format(searchURL, strValue));



            JObject JsonData = Newtonsoft.Json.JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(resopnse.Result);

            ResultDetail resultDetail = new ResultDetail();

            resultDetail.Title = JsonData["title"].ToString();
            resultDetail.PublicationDate = JsonData["published_date"].ToString();
            resultDetail.PublicationName = JsonData["publisher"].ToString();


            string Authors = string.Empty;

            if (JsonData["z_authors"] != null)
            {
               for (int i = 0; i < JsonData["z_authors"].Count(); i++)
               {
                  if (Authors.Length > 0)
                  {
                     Authors += ";";
                  }

                  Authors += JsonData["z_authors"][i]["family"].ToString();
               }
            }
            resultDetail.Authors = Authors;

            if (JsonData["is_oa"].ToString() == "True")
            {
               if (JsonData["best_oa_location"] != null)
               {
                  if (JsonData["best_oa_location"]["url"] != null)
                  {
                     resultDetail.URI = JsonData["best_oa_location"]["url"].ToString();
                  }
                  else if (JsonData["best_oa_location"]["url_for_pdf"] != null)
                  {
                     resultDetail.URI = JsonData["best_oa_location"]["url_for_pdf"].ToString();
                  }
               }
            }

            System.Console.WriteLine(string.Format("Title:{0};Authors:{1};URI:{2};publisher:{3};published_date{4}"
               , resultDetail.Title, resultDetail.Authors, resultDetail.URI, resultDetail.PublicationName, resultDetail.PublicationDate));

            //if (resopnse.StatusCode == System.Net.HttpStatusCode.NotFound)
            //{

            //   System.Console.WriteLine(resopnse.Result);
            //}
            //else
            //{
            //   System.Console.WriteLine(resopnse.Result);

            //}
         }
         catch (Exception ex)
         {

            throw;
         }
      }


   }
}
