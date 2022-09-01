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
using System.Collections.Specialized;
using System.Threading;
using WinEventHook;
using System.Net.Security;
using ComponentSpace.SAML2.Assertions;
using System.Web;

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

         #region old 

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


         #endregion

         #region Regex
         //string str1 = " Felidae--Juvenile literature.--Juvenile literature.; Cats. ";

         //foreach (char item in str1.ToCharArray())
         //{
         //   System.Console.WriteLine(item);
         //}

         //string[] allowTypes = new string[] { "Shell", "agg", "histShell", "histNewsAgg", "apps", "Historic", "category", "obits" };


         //System.Console.WriteLine(allowTypes.Any(a=>a.ToLower()=="Hello".ToLower()));

         //System.Console.WriteLine(allowTypes.Any(a => a.ToLower() == "histShell".ToLower()));
         // ParseHtml();

         //string strURL = "https://1.eduvision.tv";

         //Uri uri = new Uri(strURL);

         //System.Console.WriteLine((new Uri("https://1.eduvision.tv")).Host);
         //System.Console.WriteLine((new Uri("https://eduvision.tv")).Host);
         //System.Console.WriteLine((new Uri("https://www.eduvision.tv")).Host);

         //System.Console.WriteLine((new Uri("https://org.eduvision.tv")).Host);

         //    (https ?| ftp | file)://[-A-Za-z0-9+&@#/%?=~_|!:,.;]+[-A-Za-z0-9+&@#/%=~_|]

         //System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("https?://[a-zA-Z.]*eduvision.tv");



         //System.Console.WriteLine("https://org.eduvision.tv {0}",reg.IsMatch("https://org.eduvision.tv"));
         //System.Console.WriteLine("https://eduvision.tv {0}", reg.IsMatch("https://eduvision.tv"));

         //System.Console.WriteLine("http://eduvision.tv {0}", reg.IsMatch("http://eduvision.tv"));

         //System.Console.WriteLine("https://123123on.tv {0}", reg.IsMatch("https://123123on.tv"));

         //try
         //{

         //         var dirs = System.IO.Directory.EnumerateDirectories(@"D:\doc\Research\EDI");

         //	foreach (var item in dirs)
         //	{
         //            System.Console.WriteLine(item);

         //		if (item.Contains("Brodart"))
         //		{
         //               System.IO.Directory.Delete(item, true);
         //            }
         //         }

         //         //System.IO.Directory.Delete(@"D:\doc\Research\EDI\Fw_ More Brodart EDI fyi  ", true);
         //      }
         //catch (Exception ex)
         //{

         //         System.Console.WriteLine(ex.Message);
         //      }




         //      TestEduvision();

         #endregion

         #region Network

         //NetworkAdapters.GetLocalIPAddress();

         #endregion

         #region Print 

         //PrintTest.FindSharePrinter("192.168.21.179", "Brother DCP-L2535DW series Printer");

         //      PrintTest.FindSharePrinter("192.168.21.95", "Brother DCP-L2535DW series Printer195");
         //      PrintTest.Load();

         //      System.Console.WriteLine("---------------------------");



         //      PrintTest.WMILoad();

         #endregion


         //Win32Test.GetWindow();

         //var hook = new WindowEventHook();
         //hook.EventReceived += (s, e) =>
         //    Console.WriteLine(Enum.GetName(typeof(WindowEvent), e.EventType));
         //hook.HookGlobal();
         //Console.Read();

         #region findWindow



         // Win32Test.MatchWord2013PrintPage();


         //System.Threading.Timer _timer = new System.Threading.Timer((_)=> { Win32Test.MatchWord2013PrintPage(); }, null, 500, 500);


         #endregion


         //string clientID = "de29abdf-a2fa-4e79-92f8-5bf942f877a9";
         //string clientSecret = "67f49c94-9291-4ce1-b55b-125d1ea6c307";


         //Console.WriteLine($"Base64Encode:{Base64Encode(clientID+":"+ clientSecret)}");

         //Console.WriteLine($"token:{GetToken()}");


         //         string xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
         //<Assertion ID=""_dc5d2294-4441-406a-ad1a-5cb486310100"" IssueInstant=""2022-06-09T21:57:10.781Z"" Version=""2.0"" xmlns=""urn:oasis:names:tc:SAML:2.0:assertion"" xmlns:samlp=""urn:oasis:names:tc:SAML:2.0:protocol"">
         //<Issuer>https://sts.windows.net/781241c9-17cf-4620-84af-3520024063a8/</Issuer>
         //<Signature xmlns=""http://www.w3.org/2000/09/xmldsig#""><SignedInfo>
         //<CanonicalizationMethod Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#"" /><SignatureMethod Algorithm=""http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"" />
         //<Reference URI=""#_dc5d2294-4441-406a-ad1a-5cb486310100""><Transforms><Transform Algorithm=""http://www.w3.org/2000/09/xmldsig#enveloped-signature"" />
         //<Transform Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#"" /></Transforms><DigestMethod Algorithm=""http://www.w3.org/2001/04/xmlenc#sha256"" />
         //<DigestValue>YKDhr03PRsrh5rp8EW9aA/S2p1jp8iw/pv0JBSnKjSw=</DigestValue></Reference></SignedInfo>
         //<SignatureValue>ZnrTyVl4eIpVoOSqT5WLPHbgjKBmzcf8TrYryNUeLL6m6oOYYtGBDz/qeW5/qZVF+mlCS6Vp9iL0vOJrHBL+/j6mpvsiFzyRJYp0mTHiXs9R1Z6doeBesH0zx266n+ZbAEaX3LJjM/q5YNM1A652mTilb3RaqnGt/5KYMGjYWZ5RdoK1aUhXTTzMEZhne0/gbmdhXJXQHSljZHTm0hpOok6oS3Q5A75AaHOrxp39+C+WqvNlBN1wOd9iiiFpMCasj+w0w0YN0JQfTAWU1rhw11TEkE4POJWofOeBhamXxS33Lzf8thr0mHh2dSk6S6o3yTLHhim2Vv4iJhtL16uRSA==</SignatureValue><KeyInfo><X509Data><X509Certificate>MIIC8DCCAdigAwIBAgIQVacbcg0fIY1HOpTPngLhjTANBgkqhkiG9w0BAQsFADA0MTIwMAYDVQQDEylNaWNyb3NvZnQgQXp1cmUgRmVkZXJhdGVkIFNTTyBDZXJ0aWZpY2F0ZTAeFw0yMjA2MDcxNDM2NDFaFw0yNTA2MDcxNDM2NDFaMDQxMjAwBgNVBAMTKU1pY3Jvc29mdCBBenVyZSBGZWRlcmF0ZWQgU1NPIENlcnRpZmljYXRlMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAxh9CTpspjJ9oAVayT+4O71eBh7cA2pUWTo9TreIGaUAURPQwaxWPeov5diZNqnv/m5OFUKZmanje68hwRIefr8iPUqo1nAz7hdM8vDhevs9YJtVZ7oMzOv0XJDaXCmCHpmk94rxDmSzxRyNn/q5gG/rN4IGYTtQZUKGrK3J+NFMC8+6g3m7Kp3+glDOuFFKxroUwMCf76vyuo7hHF3a3t5MqnyMmO+bhQxgx1mbSY0BVuj9PcmcnW66uUZF9/Mao6F8JxT+4QHUAekrfEFfppFWvWhPDTfZyqunBKJgUBpZgQlhgYiNEkha71uwMWnyjwcHGZLPFaMydFrTmRJEdgQIDAQABMA0GCSqGSIb3DQEBCwUAA4IBAQCi3tpf+lWrDaA5NYwV5lR+EVRJP2MK3UHM2COWWRF5Q7Fw+sHLDmhXPbkpVN9Vu1sa6noxbgoHhBQHbMXsH48MRKe6XeJSFG/dtjobRr9ZxZFuI08vA2Nio+kJnG0jz+Cur08dLXs6EpEx7RdE0g0PN2HrxRNcJUsRHo3I9lLSdsL0aYNP9yzdB3LQN4jn3Wx7D4LAZ6rKTlXdrmQXyDIRNlKQ9zNHRDYcgb0vT29tJj2Lk/7ZMN8cnOmltsJ3rW9XoEhfWXfyX8lu5K6/W5uhHZX5A1BamNmG94VnI8d3aQZ77WMAe7DyVA7Shjm0U9EVCsU8T/2pWUXhVZz5bjT9</X509Certificate></X509Data></KeyInfo></Signature><Subject><NameID Format=""urn:oasis:names:tc:SAML:1.1:nameid-format:emailAddress"">NeilGoodliffe@gpcsd.ca</NameID><SubjectConfirmation Method=""urn:oasis:names:tc:SAML:2.0:cm:bearer""><SubjectConfirmationData InResponseTo=""_3c719782-1baa-4763-8a80-67ff1bec5dfa"" NotOnOrAfter=""2022-06-09T22:57:10.641Z"" Recipient=""https://gpcsd.insigniails.com/ILS/SAML/AssertionConsumerService.aspx?binding=urn%3Aoasis%3Anames%3Atc%3ASAML%3A2.0%3Abindings%3AHTTP-POST"" /></SubjectConfirmation></Subject>
         //<Conditions NotBefore=""2022-06-09T21:52:10.641Z"" NotOnOrAfter=""2022-06-09T22:57:10.641Z"">
         //<AudienceRestriction><Audience>https://gpcsd.insigniails.com/ILS/</Audience></AudienceRestriction></Conditions>
         //<AttributeStatement><Attribute Name=""http://schemas.microsoft.com/identity/claims/tenantid""><AttributeValue>781241c9-17cf-4620-84af-3520024063a8</AttributeValue></Attribute>
         //<Attribute Name=""http://schemas.microsoft.com/identity/claims/objectidentifier""><AttributeValue>80ccbc96-12b4-4700-a235-4ce257b4a5a7</AttributeValue></Attribute>
         //<Attribute Name=""http://schemas.microsoft.com/identity/claims/identityprovider""><AttributeValue>https://sts.windows.net/781241c9-17cf-4620-84af-3520024063a8/</AttributeValue>
         //</Attribute><Attribute Name=""http://schemas.microsoft.com/claims/authnmethodsreferences""><AttributeValue>http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/password</AttributeValue>
         //<AttributeValue>http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/x509</AttributeValue><AttributeValue>http://schemas.microsoft.com/claims/multipleauthn</AttributeValue></Attribute>
         //<Attribute Name=""givenname""><AttributeValue>Neil</AttributeValue></Attribute><Attribute Name=""surname""><AttributeValue>Goodliffe</AttributeValue></Attribute>
         //<Attribute Name=""emailaddress"">
         //<AttributeValue>NeilGoodliffe@gpcsd.ca</AttributeValue></Attribute></AttributeStatement><AuthnStatement AuthnInstant=""2022-06-09T18:23:29.046Z"" SessionIndex=""_dc5d2294-4441-406a-ad1a-5cb486310100"">
         //<AuthnContext><AuthnContextClassRef>urn:oasis:names:tc:SAML:2.0:ac:classes:Password</AuthnContextClassRef></AuthnContext></AuthnStatement></Assertion>";


         //         XmlDocument xmlDocument = new XmlDocument();
         //         xmlDocument.LoadXml(xml);

         //         XmlElement xmlElement = xmlDocument.DocumentElement;

         //         SAMLAssertion samlAssertion = null;

         //         samlAssertion = new SAMLAssertion(xmlElement);

         //         string userName = samlAssertion.GetAttributeValue("emailaddress");


         //         Console.WriteLine($"userName:{userName}");

         //string url1 = "http://stimson.contentdm.oclc.org/cdm/ref/collection/p15290coll4/id/259";

         //string encodeurl1 = "http%3A%2F%2Fstimson.contentdm.oclc.org%2Fcdm%2Fref%2Fcollection%2Fp15290coll4%2Fid%2F259";

         //string url2 = "http://search.ebscohost.com/login.aspx?direct=true&scope=site&db=nlebk&db=nlabk&AN=281922";

         //string encodeurl2 = "http%3A%2F%2Fsearch.ebscohost.com%2Flogin.aspx%3Fdirect%3Dtrue%26scope%3Dsite%26db%3Dnlebk%26db%3Dnlabk%26AN%3D281922";


         //string url3 = "http://firstsearch.oclc.org/journal=0459-7222;screen=info;ECOIP";

         //string encodeurl3 = "http%3A%2F%2Ffirstsearch.oclc.org%2Fjournal%3D0459-7222%3Bscreen%3Dinfo%3BECOIP";

         //string url4 = "https://search-ebscohost-com.stimson.idm.oclc.org/login.aspx?direct=true&db=nlebk&AN=2523058&site=ehost-live";

         //string encodeurl4 = "https%3A%2F%2Fsearch-ebscohost-com.stimson.idm.oclc.org%2Flogin.aspx%3Fdirect%3Dtrue%26db%3Dnlebk%26AN%3D2523058%26site%3Dehost-live";


         //Console.WriteLine($"{url1}:{HttpUtility.UrlEncode(url1)}::: {HttpUtility.UrlEncode(url1) == encodeurl1}");

         //Console.WriteLine($"{url3}:{HttpUtility.UrlEncode(url3)}::: {HttpUtility.UrlEncode(url3) == encodeurl3}");

         TestXmlParse();

         Console.ReadLine();
      }

      public static string Base64Encode(string source)
      {
         return Base64Encode(Encoding.UTF8, source);
      }

      public static string Base64Encode(Encoding encodeType, string source)
      {
         string encode = string.Empty;
         byte[] bytes = encodeType.GetBytes(source);
         try
         {
            encode = Convert.ToBase64String(bytes);
         }
         catch
         {
            encode = source;
         }
         return encode;
      }





      private void learn360Test()
		{
         string eBookLink = "https://learn360.infobase.com/PortalPlaylists.aspx?wID=266625&xtid=96801";

         string downloadAddress = string.Empty;

         string baseNewLearn360Link = NewLearn360Link(eBookLink);
         if (!string.IsNullOrEmpty(baseNewLearn360Link) && eBookLink.Contains("xtid="))
         {
            string videoID = eBookLink.Substring(eBookLink.IndexOf("xtid=") + "xtid=".Length);
            if (videoID.IndexOf("&") == 0)
            {
               videoID = null;
            }
            else if (videoID.IndexOf("&") > 0)
            {
               videoID = videoID.Substring(0, videoID.IndexOf("&"));
            }
            if (!string.IsNullOrWhiteSpace(videoID))
            {
               downloadAddress = baseNewLearn360Link.ToLower();
               if (downloadAddress.Contains("portalplaylists.aspx?"))
               {
                  downloadAddress = baseNewLearn360Link.Replace(baseNewLearn360Link.Substring(downloadAddress.IndexOf("portalplaylists.aspx?")), "");
               }
               else
               {
                  downloadAddress = baseNewLearn360Link;
               }
               downloadAddress += "image/" + videoID;
            }
         }


         if (!string.IsNullOrWhiteSpace(downloadAddress))
         {
            //string filePath = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "~imageCT" + itemID + ".JPG";
            //alivya add contentType check logic at 2017-07-28, ver 8.1.4.
            bool isImageContent = false;
            System.Net.HttpWebRequest httpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(downloadAddress);
            System.Net.WebResponse webResponse = httpWebRequest.GetResponse();
            if (webResponse.ContentType.ToLower().StartsWith("image"))
               isImageContent = true;
            httpWebRequest.Abort();
            webResponse.Close();
            string fileName = "~imageCT2269399.JPG";
            if (isImageContent)
            {
               System.Net.WebClient webClient = new System.Net.WebClient();
               webClient.DownloadFile(downloadAddress, fileName);
               FileInfo info = new FileInfo(fileName);
               if (info.Exists)
               {
                  if (info.Length > 1000)
                  {
                     FileStream stream = new FileStream(fileName, FileMode.Open);
                     int length = (int)stream.Length;
                     byte[] buffer = new byte[length];
                     stream.Read(buffer, 0, buffer.Length);
                     stream.Close();

                  }
               }
            }
         }
      }
      /// <summary>
      /// 计算小数取整问题
      /// </summary>
      private void ComputeFloor()
      {
         double x1 = 5.124567;
         double x2 = 5.125111;
         double x3 = 5.126111;

         Console.WriteLine($"{x1}:{(x1 * 100)} {Convert.ToInt32(x1 * 100)} {(Convert.ToInt32((x1 * 100)) / 100f).ToString("0:F")}");
         Console.WriteLine($"{x2}: {(x2 * 100).ToString()} {(Convert.ToInt32((x2 * 100)) / 100f).ToString("0:F")}");
         Console.WriteLine($"{x2}:  {string.Format("{0:0.00}", Math.Floor(x2 * 100) / 100)}");
         Console.WriteLine($"{x3}: {(Convert.ToInt32((x3 * 100)) / 100f).ToString("0:F")}");
      }

      static string NewLearn360URL = "https://learn360.infobase.com|https://fod.infobase.com|https://avod.infobase.com|https://cvod.infobase.com|http://learn360.infobase.com|http://fod.infobase.com|http://avod.infobase.com|http://cvod.infobase.com";
      private static string NewLearn360Link(string strLink)
      {
         string newLearn360 = string.Empty;
         if (!string.IsNullOrEmpty(NewLearn360URL))
         {
            var separator = new char[1];
            separator[0] = '|';
            var baseURLs = NewLearn360URL.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            if (baseURLs.Length > 0)
            {
               foreach (string baseURL in baseURLs)
               {
                  if (strLink.ToLower().IndexOf(baseURL.ToLower()) == 0)
                  {
                     newLearn360 = baseURL + "/PortalPlaylists.aspx?";
                     break;
                  }
               }
            }
         }
         return newLearn360;
      }


      private static void ParseHtml()
      {
         string strHtml = "<input class=\"k-checkbox\" id=\"chkName + index + \" name=\"encodeURIComponent(value)\" type=\"checkbox\" value=\"true\" checked>";

         string Name = string.Empty;

         string ID = string.Empty;

         foreach (var item in strHtml.Split(' '))
         {
            if (item.ToLower().Contains("name=") || item.ToLower().Contains("name ="))
            {
               Name = item.Substring(item.IndexOf("=") + 1);
            }

            if (item.ToLower().Contains("id=") && item.ToLower().Contains("id ="))
            {
               ID = item.Substring(item.IndexOf("=") + 1);
            }
         }

         Console.WriteLine($"name:{Name}");
         Console.WriteLine($"{nameof(ID)}:{ID}");
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
            var responseData = RestHelper.RequestURL_GET(strURL, "Login");

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


      /// <summary>

      /// </summary>
      public static void TestXmlParse()
      {
         string strFilePath = "C:\\clickView.xml";

       

         XmlDocument xmlDoc = LoadXMLFromFile(strFilePath);


         var nodes = xmlDoc.SelectNodes("rss/channel/item");

         XmlDocument xmlDoc1 = new XmlDocument();

         xmlDoc1.LoadXml(nodes[0].OuterXml);

         var itemNode = xmlDoc1.SelectSingleNode("item");

         foreach (XmlNode itemSub in itemNode.ChildNodes)
         {
            Console.WriteLine($"itemSub:{itemSub.Name}");

            if(itemSub.Name== "pubDate")
				{
               Console.WriteLine($"mediaSub:{DateTime.Parse(itemSub.InnerText)}");
            }

				if (itemSub.Name.ToUpper().Contains("MEDIA:CONTENT"))
				{
               foreach(XmlNode mediaSub in itemSub.ChildNodes)
					{
                  Console.WriteLine($"mediaSub:{mediaSub.Name}");
               }
				}
         }

         //var node = xmlDoc1.SelectSingleNode("item/guid");

         //Console.WriteLine($"node:{node.InnerText}");


         Console.WriteLine($"total:{nodes.Count}");
         return;

         //if (xmlDoc != null)
         //{
         //   var node = xmlDoc.SelectSingleNode("resultList/fedtask/result/hitlist");
         //   if (node != null)
         //   {

         //      var totalAttribute = node.Attributes["total"];
         //      if (totalAttribute != null)
         //      {
         //         Console.WriteLine($"total:{totalAttribute.InnerText}");

         //      }

         //      var countAttribute = node.Attributes["count"];
         //      if (countAttribute != null)
         //      {
         //         Console.WriteLine($"count:{countAttribute.InnerText}");
         //      }
         //      else
         //      {
         //         return;
         //      }

         //      var firstAttribute = node.Attributes["first"];
         //      if (firstAttribute != null)
         //      {
         //         Console.WriteLine($"first:{firstAttribute.InnerText}");
         //      }
         //      else
         //      {
         //         return;
         //      }

         //      //Publication Name ? Title (245a)
         //      //Publication Date ? Pub.Date(264c)
         //      //Headline? Subtitle(245b)
         //      //Preview? Summary(520a)
         //      //Aut? Author(100a)
         //      //Rank? The relevance rank score(if we don’t use this we, can ignore it)
         //      if (node.HasChildNodes)
         //      {
         //         foreach (XmlNode childNode in node.ChildNodes)
         //         {
         //            string Title245a = string.Empty;
         //            string SubTitle245b = string.Empty;
         //            string Authoer100a = string.Empty;
         //            string Summary520a = string.Empty;
         //            string pubName264b = string.Empty;
         //            string pubDate264c = string.Empty;
         //            string url = string.Empty;

         //            var findNode = childNode.SelectSingleNode("text[@type='title']");

         //            if (findNode != null)
         //            {
         //               Title245a = findNode.InnerText;
         //            }



         //            findNode = childNode.SelectSingleNode("publication-name");

         //            if (findNode != null)
         //            {
         //               pubName264b = findNode.InnerText;
         //            }


         //            findNode = childNode.SelectSingleNode("publication-date");
         //            if (findNode != null)
         //            {
         //               pubDate264c = findNode.InnerText;
         //            }

         //            findNode = childNode.SelectSingleNode("field[@name='hed']");
         //            if (findNode != null)
         //            {
         //               SubTitle245b = findNode.InnerText;
         //            }

         //            findNode = childNode.SelectSingleNode("field[@name='aut']");
         //            if (findNode != null)
         //            {
         //               Authoer100a = findNode.InnerText;
         //            }

         //            findNode = childNode.SelectSingleNode("field[@name='preview']");
         //            if (findNode != null)
         //            {
         //               Summary520a = findNode.InnerText;
         //            }

         //            findNode = childNode.SelectSingleNode("openurl/url");
         //            if (findNode != null)
         //            {
         //               url = findNode.InnerText;
         //            }


         //            Console.WriteLine($"\nTitle245a:{Title245a}");
         //            Console.WriteLine($"SubTitle245b:{SubTitle245b}");
         //            Console.WriteLine($"Authoer100a:{Authoer100a}");
         //            Console.WriteLine($"Summary520a:{Summary520a}");
         //            Console.WriteLine($"pubName264b:{pubName264b}");
         //            Console.WriteLine($"pubDate264c:{pubDate264c}");
         //            Console.WriteLine($"url:{url}");

         //            Console.ReadKey();

         //         }
         //      }




         //   }
         //}

       
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


      public static void TestEduvision()
		{
         string url = "https://api12.eduvision.tv/restapi.ashx/v11/user/authenticate";

         var body = @"<Request>" + "\n" +
                        @"<User>" + "\n" +
                        @"<UserName>oneplaceTest</UserName>" + "\n" +
                        @"<FirstName>Girijaa</FirstName>" + "\n" +
                        @"<LastName>Doraiswamy</LastName>" + "\n" +
                        @"<Code>0149</Code>" + "\n" +
                        @"<Email>girijaad@jdlhorizons.com</Email>" + "\n" +
                        @"</User>" + "\n" +
                        @"</Request>";


         NameValueCollection headers = new NameValueCollection();


         headers["REST-APIUser--Token"] = "Z2lyaWphYWFwaWtleTpnaXJpamFh";
         headers["APP-User-ID"] = "MQ==";


         IHttpRequest httpRequest = new HttpRequest();

         IResopnse resopnse = httpRequest.PostXML(url, string.Empty, headers, body);

         XmlDocument xmlDocument = new XmlDocument();
     
         xmlDocument.LoadXml(resopnse.Result);

         var userNode = xmlDocument.SelectSingleNode("Response/User");


         var findNode = userNode.SelectSingleNode("AuthenticationStatus");

         System.Console.WriteLine(findNode.InnerText);


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
