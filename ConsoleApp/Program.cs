using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using eCommerceExample;
using eCommerceExample.CA;
using eCommerceExample.Models;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic;
using System.Net;
using RestSharp;

namespace ConsoleApp
{
    class Program
    {
      public class LibrarySetup
      {
         public string LibraryID { get; set; }

         public List<string> acronyms = new List<string>();
      }

      public class TestModel
      {
         public string Test { get; set; }
         public string LibraryID { get; set; }

         public TestModel(string a,LibrarySetup librarySetup)
         {
            Test = a;
            LibraryID = librarySetup.LibraryID;
         }

         public TestModel(string a, string LibraryID):this(a,new LibrarySetup { LibraryID =LibraryID})
         {

         }
      }

      static void Main(string[] args)
        {


         //TestLibrarySetup("0001|IPAW,22N2,AAAL,AAL4-EEDT||0002|IPAW,22N2,AAAL");

         //TestLibrarySetup("0001|IPAW,22N2,AAAL,AAL4-EEDT");


         //TestLibrarySetup("0001|IPAW");


         //TestLibrarySetup("0001|IPAW||");


         //TestLibrarySetup("0001|IPAW||0002");

         //TestLibrarySetup("0001||0002");


         //TestXmlParse();
         //TestReg();


         //System.Console.WriteLine(Operators.CompareString("aaa", "Aaa", false));
         //System.Console.WriteLine(Operators.CompareString("aaa", "Aaa", true));
         //System.Console.WriteLine(string.Compare("aaa","Aaa", true));
         //System.Console.WriteLine(string.Compare("aaa", "Aaa", false));

         //System.Console.WriteLine("aaa"=="Aaa"?0:1);


         //bool[] array4 = new bool[1] { true };

         //bool[] array5 = new bool[] { true };
         ////string test = "\r\n";


         //object a = "aaa";
         //object b = "aaa";


         ////System.Console.WriteLine(test.IndexOf("\r\n"));
         ////System.Console.WriteLine(Strings.InStr(test,"\r\n"));

         //System.Console.WriteLine(Operators.CompareObjectNotEqual("0",
         //            "1", TextCompare: false));

         //System.Console.WriteLine(Operators.OrObject(false, true));
         // System.Console.WriteLine(Operators.OrObject(false, false));
         //System.Console.WriteLine(Operators.OrObject(true, false));



         //TestLoginNewsBank();
         // TestUnPaywall();

         // var test = new TestModel("1", "2");

         //System.Console.WriteLine($"{test.Test}:{test.LibraryID}");

         //LibrarySetup librarySetup1 = new LibrarySetup()
         //{
         //   LibraryID = "123123",
         //   acronyms = new List<string>()
         //};

         //List<LibrarySetup> LibrarySetups = new List<LibrarySetup>();

         //LibrarySetups.Add(librarySetup1);

         //testLibrarySetup(librarySetup1);


         //Console.WriteLine($"LibraryID:{librarySetup1.LibraryID}");
         //Console.WriteLine($"acronyms Count:{librarySetup1.acronyms.Count}");
         //Console.WriteLine($"acronyms :{string.Join(",", librarySetup1.acronyms)}{Environment.NewLine}");

         //Console.WriteLine($"TestLibrarySetup count:{LibrarySetups.Count}");

         //foreach (var lib in LibrarySetups)
         //{
         //   Console.WriteLine($"LibraryID:{lib.LibraryID}");
         //   Console.WriteLine($"acronyms Count:{lib.acronyms.Count}");
         //   Console.WriteLine($"acronyms :{string.Join(",", lib.acronyms)}{Environment.NewLine}");
         //}



         //         string str = @"<?xml version=""1.0""?>
         //<marc:record xmlns:marc=""http://www.loc.gov/MARC21/slim"">
         //   <marc:leader>nmm##22*****2i#4500</marc:leader>
         //   <marc:controlfield tag=""007"">cr#cnannnuuuuu</marc:controlfield>
         //   <marc:controlfield tag=""008"">201030s    |||####|o##j########eng#d</marc:controlfield>
         //   <marc:datafield tag=""245"" ind1="""" ind2="""">
         //      <marc:subfield code=""a"">Airplane of the Year</marc:subfield></marc:datafield>
         //   <marc:datafield tag=""264"" ind1="""" ind2=""1"">
         //      <marc:subfield code=""b"">Air  Space</marc:subfield><marc:subfield code=""c"">2019</marc:subfield></marc:datafield>
         //   <marc:datafield tag=""520"" ind1=""#"" ind2="""">
         //      <marc:subfield code=""a"">This September 11-15, at the National Championship Air Races in Reno, Nevada, a couple dozen magnificently restored airplanes will gather once again to be evaluated by judges and admired by fans. Those who attend will be able to stroll among some pretty special aircraft, hear the owners and restorers tell their stories, and watch the presentation of trophies by aviation heroes. (In 2003, Bob Hoover, above at left, handed out a few.) They'll also get the chance to vote for their favorite</marc:subfield></marc:datafield>
         //<marc:datafield tag=""856"" ind1=""4"" ind2=""""><marc:subfield code=""u"">http://docs.newsbank.com/openurl?ctx_ver=z39.88-2004&amp;rft_id=info:sid/iw.newsbank.com:AFNB&amp;rft_val_format=info:ofi/fmt:kev:mtx:ctx&amp;rft_dat=document_id:17616E7338A16BD8&amp;svc_dat=primo_insignia&amp;req_dat=0FE810AEE1BF00E4</marc:subfield>
         //      <marc:subfield code=""y"">Click here to view</marc:subfield></marc:datafield></marc:record>";

         //         try
         //         {
         //            var XMLDoc = new XmlDocument();
         //            XMLDoc.LoadXml(str);
         //         }
         //         catch (Exception ex)
         //         {

         //            throw ex; 
         //         }


         //string str1 = " Felidae--Juvenile literature.--Juvenile literature.; Cats. ";

         //foreach (char item in str1.ToCharArray())
         //{
         //   System.Console.WriteLine(item);
         //}


         double x1 = 5.124567;
         double x2 = 5.125111;
         double x3 = 5.126111;

         Console.WriteLine($"{x1}:{(x1 * 100)} {Convert.ToInt32(x1 * 100)} {(Convert.ToInt32((x1*100))/100f).ToString("0:F")}");
         Console.WriteLine($"{x2}: {(x2 * 100).ToString()} {(Convert.ToInt32((x2 * 100)) / 100f).ToString("0:F")}");
         Console.WriteLine($"{x2}:  {string.Format("{0:0.00}", Math.Floor(x2 * 100) / 100)}");
         Console.WriteLine($"{x3}: {(Convert.ToInt32((x3 * 100)) / 100f).ToString("0:F")}");



         Console.ReadKey();
      }

      private static XmlDocument LoadXMLFromFile(string strFileNamePath)
      {
         if (System.IO.File.Exists(strFileNamePath))
         {
            Console.WriteLine("file exist");
         }
         else
         {
            Console.WriteLine("file not exist");
            return null;
         }

         using (FileStream stream = System.IO.File.OpenRead(strFileNamePath))
         {
            using (StreamReader sr = new StreamReader(stream, System.Text.Encoding.Default))
            {
               XmlDocument xmlDoc = new XmlDocument();
               xmlDoc.Load(sr);

               return xmlDoc;
            }
           
         }


      }

      private static XmlDocument LoadXMLFromURL(string strURL)
      {
         try
         {
            var responseData = RequestURL_GET(strURL, "Login");

            if (responseData.XmlDoc != null)
            {
               return responseData.XmlDoc;
            }
            else
            {
               throw new Exception(string.Format("step:Login,StatusCode:{0}-{1},Result:{2}"
                           , (int)responseData.StatusCode, responseData.StatusCode.ToString(), "null"));
            }


         }
         catch (Exception)
         {

            throw;
         }
      }


      #region NewsBank
      private static void TestLoginNewsBank()
      {
         //string url = string.Format("https://docs.newsbank.com/gateway2/platform/login?api_key=0C23C4311286439C8A9A6004A07DF834&cust_auth_type=userpw&cust_auth=Insignia123|Insignia123");
         string url = string.Format("https://docs.newsbank.com/gateway2/platform/login?api_key=0C23C4311286439C8A9A6004A07DF834&cust_auth_type=userpw&cust_auth=38MWRLIB|2001CFSC");



         try
         {

            XmlDocument xmlDocument =  LoadXMLFromURL(url);
            string strFilePath = "C:\\product.xml";
            //XmlDocument xmlDocument = LoadXMLFromFile(strFilePath);

            if (xmlDocument != null)
            {
               var node = xmlDocument.SelectSingleNode("authentication/authenticationtoken");
               if (node != null)
               {
                  System.Console.WriteLine(node.InnerText);
               }

               var nodeProducts = xmlDocument.SelectNodes("authentication/productinfo/product");
               if (nodeProducts != null)
               {
                  string strProductNames = string.Empty;
                  foreach (XmlNode nodeProduct in nodeProducts)
                  {
                     XmlNode nodeProductName = nodeProduct.SelectSingleNode("acronym");
                     if (nodeProductName != null)
                     {
                        strProductNames += nodeProductName.InnerText + "=====";
                     }
                  }

                  System.Console.WriteLine(strProductNames);
               }
            }
           

         }
         catch (Exception)
         {

            throw;
         }
      }

      /// <summary>

      /// </summary>
      public static void TestXmlParse()
      {
         string strFilePath = "C:\\1PAW.xml";

         XmlDocument xmlDoc = LoadXMLFromFile(strFilePath);

         if (xmlDoc != null)
         {
            var node = xmlDoc.SelectSingleNode("resultList/fedtask/result/hitlist");
            if (node != null)
            {

               var totalAttribute = node.Attributes["total"];
               if (totalAttribute != null)
               {
                  Console.WriteLine($"total:{totalAttribute.InnerText}");

               }

               var countAttribute = node.Attributes["count"];
               if (countAttribute != null)
               {
                  Console.WriteLine($"count:{countAttribute.InnerText}");
               }
               else
               {
                  return;
               }

               var firstAttribute = node.Attributes["first"];
               if (firstAttribute != null)
               {
                  Console.WriteLine($"first:{firstAttribute.InnerText}");
               }
               else
               {
                  return;
               }

               //Publication Name ? Title (245a)
               //Publication Date ? Pub.Date(264c)
               //Headline? Subtitle(245b)
               //Preview? Summary(520a)
               //Aut? Author(100a)
               //Rank? The relevance rank score(if we don’t use this we, can ignore it)
               if (node.HasChildNodes)
               {
                  foreach (XmlNode childNode in node.ChildNodes)
                  {
                     string Title245a = string.Empty;
                     string SubTitle245b = string.Empty;
                     string Authoer100a = string.Empty;
                     string Summary520a = string.Empty;
                     string pubName264b = string.Empty;
                     string pubDate264c = string.Empty;
                     string url = string.Empty;

                     var findNode = childNode.SelectSingleNode("text[@type='title']");

                     if (findNode != null)
                     {
                        Title245a = findNode.InnerText;
                     }



                     findNode = childNode.SelectSingleNode("publication-name");

                     if (findNode != null)
                     {
                        pubName264b = findNode.InnerText;
                     }


                     findNode = childNode.SelectSingleNode("publication-date");
                     if (findNode != null)
                     {
                        pubDate264c = findNode.InnerText;
                     }

                     findNode = childNode.SelectSingleNode("field[@name='hed']");
                     if (findNode != null)
                     {
                        SubTitle245b = findNode.InnerText;
                     }

                     findNode = childNode.SelectSingleNode("field[@name='aut']");
                     if (findNode != null)
                     {
                        Authoer100a = findNode.InnerText;
                     }

                     findNode = childNode.SelectSingleNode("field[@name='preview']");
                     if (findNode != null)
                     {
                        Summary520a = findNode.InnerText;
                     }

                     findNode = childNode.SelectSingleNode("openurl/url");
                     if (findNode != null)
                     {
                        url = findNode.InnerText;
                     }


                     Console.WriteLine($"\nTitle245a:{Title245a}");
                     Console.WriteLine($"SubTitle245b:{SubTitle245b}");
                     Console.WriteLine($"Authoer100a:{Authoer100a}");
                     Console.WriteLine($"Summary520a:{Summary520a}");
                     Console.WriteLine($"pubName264b:{pubName264b}");
                     Console.WriteLine($"pubDate264c:{pubDate264c}");
                     Console.WriteLine($"url:{url}");

                     Console.ReadKey();

                  }
               }




            }
         }

       
      }

      private static void TestLibrarySetup(string test)
      {
         List<LibrarySetup> LibrarySetups = new List<LibrarySetup>();

         foreach (string item in test.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries))
         {

            var librarySetupArray = item.Split('|');

            if (librarySetupArray != null && librarySetupArray.Length > 1)
            {
               LibrarySetup librarySetup = new LibrarySetup()
               {
                  LibraryID = librarySetupArray[0],
                  acronyms = new List<string>(librarySetupArray[1].Split(','))
               };
               LibrarySetups.Add(librarySetup);
            }
            else
            {
               if (string.IsNullOrEmpty(item) == false)
               {
                  LibrarySetup librarySetup = new LibrarySetup()
                  {
                     LibraryID = item,
                     acronyms = new List<string>()
                  };
                  LibrarySetups.Add(librarySetup);
               }

            }
            


         }
         Console.WriteLine($"Source:{test}");

         Console.WriteLine($"TestLibrarySetup count:{LibrarySetups.Count}");

         foreach (var lib in LibrarySetups)
         {
            Console.WriteLine($"LibraryID:{lib.LibraryID}");
            Console.WriteLine($"acronyms Count:{lib.acronyms.Count}");
            Console.WriteLine($"acronyms :{string.Join(",", lib.acronyms)}{Environment.NewLine}");
         }

         Console.ReadKey();
      }

      private static void testLibrarySetup(LibrarySetup librarySetup)
      {
         librarySetup.acronyms.Add("123123");

         librarySetup.acronyms.Add("12312312sdafsad");
      }

      #endregion






      class ResponseModel
      {
         public XmlDocument XmlDoc { get; set; }

         public HttpStatusCode StatusCode { get; set; }
      }

      /// <summary>
      /// if error  throw ex 
      /// </summary>
      /// <param name="URL"></param>
      /// <param name="ActionStep">action step</param>
      /// <returns></returns>
      private static ResponseModel RequestURL_GET(string URL, string ActionStep)
      {
         int action = 3;
         try
         {
            if (action == 1)
            {
               var client = new RestClient(URL);
               client.Timeout = -1;
               var request = new RestRequest(Method.GET);
               IRestResponse response = client.Execute(request);

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
            else if(action == 2)
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

      


      


      public static string CreateInvoice()
      {
         return string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now);
      }


      public static void TestReg()
      {
         string[] sortValues =
         new string[] { "t02titles", "t02aut;hors", "t02subjects", "t02callno"
            , "t02isbn", "t02seriestitle", "t02date'>published", "relevance", "t110ccode", "titles","Relevance%200%200%20-%20-" };

        // Regex reg = new Regex(@"^[A-Za-z0-9]+$");


         Regex reg = new Regex(@"^[\u4E00-\u9FA5A-Za-z0-9_';=]+$");

         foreach (string item in sortValues)
         {
            var result = reg.IsMatch(item) ? "match" : "notmatch";
            System.Console.WriteLine($"{item}:{result}");
         }

         foreach (string item in sortValues)
         {
            var result = Regex.IsMatch(item,";") ? "match" : "notmatch";
            System.Console.WriteLine($"{item}:{result}");
         }
      }

      public static void TestMerchant()
      {
         //CanadaPurchaseTest.Test(Global.StoreName,Global.Token,string.Empty);

         //   TestCanadaIDebitPurchase.Test();

         //int unitp5 = 100 / 5;

         //int unitp4 = 100 / 4;

         //int unitp3 = 100 / 3;

         //int unitp6 = 100 / 6;

         //Console.WriteLine("100 / 5:" + unitp5);
         //Console.WriteLine("100 / 4:" + unitp4);
         //Console.WriteLine("100 / 3:" + unitp3);
         //Console.WriteLine("100 / 6:" + unitp6);
         //for (int i = 0; i < 10; i++)
         //{
         //   Console.WriteLine(string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now));
         //}


         //string s = "999998.12";

         //Console.WriteLine($"{s}:{Convert.ToInt32(s.Substring(0, s.IndexOf(".")))}");

         //string s1 = "999998";

         //Console.WriteLine($"{s1}:{Convert.ToInt32(s1.Substring(0, s1.IndexOf(".")>0?s1.IndexOf("."):s1.Length))}");


         var m = new Merchant("store5", "yesguy", Merchant.EnvironmentType.QA, Merchant.ProcessingCountryType.CA);

         Transaction transaction = new Transaction
         {
            Merchant = m,
            OrderId = "201907101612318560",
            Amount = "11.00",
            TxnNumber = "858400-0_14",
            CrtpyType = "7",
            CurrentTransactionType = Transaction.TransactionType.Refund,
         };

         MonerisTransacton monerisTransacton = new MonerisTransacton();
         var response = monerisTransacton.PerformTransaction(transaction);

         JavaScriptSerializer jss = new JavaScriptSerializer();
         string myJson = jss.Serialize(response);

         Console.WriteLine(myJson);
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

           

            JObject JsonData= Newtonsoft.Json.JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(resopnse.Result);

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


      public class ResultDetail
      {
         /// <summary>
         /// For learn 360
         /// </summary>
         public long ID { get; set; }
         /// <summary>
         /// For learn 360
         /// </summary>
         public string Tags { get; set; }
         /// <summary>
         /// For learn 360
         /// </summary>
         public string CopyRight { get; set; }
         /// <summary>
         /// For learn 360
         /// </summary>
         public string GradeLevels { get; set; }

         public string Title { get; set; }
         public string Titles { get; set; }
         public string ISSN { get; set; }
         public string Authors { get; set; }
         public string Subjects { get; set; }
         public string PublicationDate { get; set; }

         /// <summary>
         /// Publisher(learn 360)
         /// </summary>
         public string PublicationName { get; set; }

         /// <summary>
         /// Full Text/description (learn 360)
         /// </summary>
         public string Body { get; set; }

         public string URI { get; set; }

         /// <summary>
         /// ProQuest
         /// </summary>
         public string ISBN { get; set; }

         /// <summary>
         /// ProQuest
         /// </summary>
         public string CallNo { get; set; }

         /// <summary>
         /// JSTOR
         /// </summary>
         public string Volume { get; set; }

         public string ImageUrl { get; set; }

         public string RecordString { get; set; }

         public string Source { get; set; }

      }
   }
}
