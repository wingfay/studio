using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp
{
	public class RestHelper
	{
      /// <summary>
      /// if error  throw ex 
      /// </summary>
      /// <param name="URL"></param>
      /// <param name="ActionStep">action step</param>
      /// <returns></returns>
      public static ResponseModel RequestURL_GET(string URL, string ActionStep)
      {
         int action = 3;
         try
         {
            if (action == 1)
            {
               var client = new RestClient(URL);

               var request = new RestRequest(URL, Method.Get);

               request.Timeout = -1;
               RestResponse response = client.ExecuteAsync(request).Result;

               if (response.StatusCode == HttpStatusCode.OK
                           || response.StatusCode == HttpStatusCode.Created
                           || response.StatusCode == HttpStatusCode.Accepted)
               {
                  XmlDocument xmlDoc = new XmlDocument();
                  xmlDoc.Load(response.Content);
                  var responseData = new ResponseModel()
                  {
                     XmlDoc = xmlDoc,
                     StatusCode = response.StatusCode,
                  };
                  return responseData;
               }
               else
               {
                  var responseData = new ResponseModel()
                  {
                     XmlDoc = null,
                     StatusCode = response.StatusCode,
                  };

                  return responseData;
               }
            }
            else if (action == 2)
            {
               HttpRequest httpRequest = new HttpRequest();
               IResopnse response = httpRequest.Get(URL);

               if (response.StatusCode == HttpStatusCode.OK
                           || response.StatusCode == HttpStatusCode.Created
                           || response.StatusCode == HttpStatusCode.Accepted)
               {
                  XmlDocument xmlDoc = new XmlDocument();
                  xmlDoc.Load(response.Result);
                  var responseData = new ResponseModel()
                  {
                     XmlDoc = xmlDoc,
                     StatusCode = response.StatusCode,
                  };
                  return responseData;
               }
               else
               {
                  var responseData = new ResponseModel()
                  {
                     XmlDoc = null,
                     StatusCode = response.StatusCode,
                  };

                  return responseData;
               }

            }
            else
            {
               try
               {



                  HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
                  httpWebRequest.Method = "GET";
                  httpWebRequest.Timeout = -1;
                  httpWebRequest.KeepAlive = true;
                  httpWebRequest.ProtocolVersion = HttpVersion.Version10;
                  httpWebRequest.ServicePoint.ConnectionLimit = 1;
                  using (HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse())
                  {
                     if (response.StatusCode == HttpStatusCode.OK
                        || response.StatusCode == HttpStatusCode.Created
                        || response.StatusCode == HttpStatusCode.Accepted)
                     {
                        using (Stream s = response.GetResponseStream())
                        {
                           using (StreamReader sr = new StreamReader(s, System.Text.Encoding.Default))
                           {
                              XmlDocument xmlDoc = new XmlDocument();
                              xmlDoc.Load(sr);
                              var responseData = new ResponseModel()
                              {
                                 XmlDoc = xmlDoc,
                                 StatusCode = response.StatusCode,
                              };
                              return responseData;
                           }
                        }
                     }
                     else
                     {
                        var responseData = new ResponseModel()
                        {
                           XmlDoc = null,
                           StatusCode = response.StatusCode,
                        };

                        return responseData;
                     }
                  }
               }
               catch (WebException ex)
               {
                  if (ex.Response != null)
                  {
                     HttpWebResponse response = (HttpWebResponse)ex.Response;
                     if (response != null)
                     {
                        using (var stream = response.GetResponseStream())
                        {
                           using (var streamReader = new StreamReader(stream, Encoding.Default))
                           {

                              throw new Exception(string.Format("step:{3},StatusCode:{0}-{1},Result:{2}"
                                 , (int)response.StatusCode, response.StatusCode.ToString(), streamReader.ReadToEnd(), ActionStep));

                           }
                        }
                     }
                  }


                  throw new Exception(string.Format("step:{0},message:{1}", ActionStep, ex.Message));

               }
               catch (Exception ex)
               {
                  throw new Exception(string.Format("step:{0},message:{1}", ActionStep, ex.Message));

               }
            }

         }
         catch (Exception)
         {

            throw;
         }

         return null;





      }
   }
}
