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

namespace ConsoleApp
{
    class Program
    {
      class LibrarySetup
      {
         public string LibraryID { get; set; }

         public List<string> acronyms = new List<string>();
      }

      static void Main(string[] args)
        {


         TestLibrarySetup("0001|IPAW,22N2,AAAL,AAL4-EEDT||0002|IPAW,22N2,AAAL");

         TestLibrarySetup("0001|IPAW,22N2,AAAL,AAL4-EEDT");


         TestLibrarySetup("0001|IPAW");


         TestLibrarySetup("0001|IPAW||");


         TestLibrarySetup("0001|IPAW||0002");


         //TestXmlParse();
         //TestReg();


         Console.ReadKey();
      }


      private static void TestLibrarySetup(string test)
      {
         List<LibrarySetup> LibrarySetups = new List<LibrarySetup>();

         foreach (string item in test.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries))
         {

            var librarySetupArray = item.Split('|');

            if(librarySetupArray!=null && librarySetupArray.Length > 1)
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
               continue;
            }



           
         }
         Console.WriteLine($"Source:{test}");

         Console.WriteLine($"TestLibrarySetup count:{LibrarySetups.Count}");

         foreach(var lib in LibrarySetups)
         {
            Console.WriteLine($"LibraryID:{lib.LibraryID}");
            Console.WriteLine($"acronyms Count:{lib.acronyms.Count}");
            Console.WriteLine($"acronyms :{string.Join(",",lib.acronyms)}{Environment.NewLine}");
         }

         Console.ReadKey();
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


      /// <summary>

      /// </summary>
      public static void TestXmlParse()
      {
         string strFilePath = "C:\\1PAW.xml";
         if (File.Exists(strFilePath))
         {
            Console.WriteLine("file exist");
         }
         else
         {
            Console.WriteLine("file not exist");
            return;
         }

         using (FileStream stream = File.OpenRead(strFilePath))
         {
            using (StreamReader sr = new StreamReader(stream, System.Text.Encoding.Default))
            {
               XmlDocument xmlDoc = new XmlDocument();
               xmlDoc.Load(sr);
               var node = xmlDoc.SelectSingleNode("resultList/fedtask/result/hitlist");
               if (node != null)
               {
 
                  var totalAttribute = node.Attributes["total"];
                  if(totalAttribute!=null)
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
                     foreach(XmlNode childNode in node.ChildNodes)
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
      }
   }
}
