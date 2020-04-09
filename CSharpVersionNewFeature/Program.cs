using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CSharpVersionNewFeature
{
   class Program
   {
      static void Main(string[] args)
      {



         TestLoginRequest();

         System.Console.ReadKey();
      }

      static private void TestLoginRequest()
      {
         EnableSecurityProtocol();

         string apiUserName = "Insignia123";
        // apiUserName = "asdfasdf";
         string apiUserPassword = "Insignia123";
         string apiKey = "0C23C4311286439C8A9A6004A07DF834";


         string url = string.Format("https://docs.newsbank.com/gateway2/platform/login?api_key={0}&cust_auth_type=userpw&cust_auth={1}|{2}"
            , apiKey, apiUserName, apiUserPassword);


         try
         {
            
           

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "Get";
            httpWebRequest.Timeout = 360000;
            httpWebRequest.KeepAlive = false;
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
                        var node = xmlDoc.SelectSingleNode("authentication/authenticationtoken");
                        if (node != null)
                        {
                           WriteLogToTxt(node.InnerText);
                        }
                       
                     }
                  }
               }
               else
               {
                  WriteLogToTxt(string.Format("StatusCode:{0}-{1},Result:{2}"
                           , (int)response.StatusCode, response.StatusCode.ToString(), "null"));
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

                        WriteLogToTxt(string.Format("StatusCode:{0}-{1},Result:{2}"
                           ,(int)response.StatusCode, response.StatusCode.ToString(), streamReader.ReadToEnd()));
                       
                     }
                  }
               }
            }


            WriteLogToTxt(ex.Message);
            
         }
         catch (Exception ex)
         {
            WriteLogToTxt(ex.Message);
           
         }
         finally
         {
            WriteLogToTxt("ImportNBCItems End");
         }


      }

      static private void EnableSecurityProtocol()
      {
         WriteLogToTxt("Runtime: " + System.Diagnostics.FileVersionInfo.GetVersionInfo(typeof(int).Assembly.Location).ProductVersion);
         WriteLogToTxt("Enabled protocols:   " + ServicePointManager.SecurityProtocol);

         WriteLogToTxt("Available protocols: ");
         Boolean platformSupportsTls12 = false;
         foreach (SecurityProtocolType protocol in Enum.GetValues(typeof(SecurityProtocolType)))
         {
            WriteLogToTxt(protocol.GetHashCode().ToString());
            if (protocol.GetHashCode() == 3072)
            {
               platformSupportsTls12 = true;
            }
         }
         WriteLogToTxt("Is Tls12 enabled: " + ServicePointManager.SecurityProtocol.HasFlag((SecurityProtocolType)3072));

         // enable Tls12, if possible
         if (!ServicePointManager.SecurityProtocol.HasFlag((SecurityProtocolType)3072))
         {
            if (platformSupportsTls12)
            {
               WriteLogToTxt("Platform supports Tls12, but it is not enabled. Enabling it now.");
               ServicePointManager.SecurityProtocol |= (SecurityProtocolType)3072;
            }
            else
            {
               WriteLogToTxt("Platform does not supports Tls12.");
            }
         }

         if (ServicePointManager.SecurityProtocol.HasFlag(SecurityProtocolType.Ssl3))
         {
            WriteLogToTxt("SSL3 is enabled. Disabling it now.");
            // disable SSL3. Has no negative impact if SSL3 is already disabled. The enclosing "if" if just for illustration.
            System.Net.ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Ssl3;
         }

         WriteLogToTxt("Enabled protocols:   " + ServicePointManager.SecurityProtocol);
      }

      static private void WriteLogToTxt(string message)
      {
         System.Console.WriteLine(message);
      }




      static private void TestExceptionThrow()
      {
         try
         {

            try
            {
               throw new Exception("Error hahahahaha");
            }
            catch
            {

               throw;
            }

         }
         catch (Exception ex)
         {

            System.Console.WriteLine(ex.Message);
         }
      }

      static private void TestStringSplit()
      {
         string names = "1123123,,";

         List<string> vs = new List<string>(names.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));

         System.Console.WriteLine(vs.Count);

         System.Console.WriteLine(string.Join("...", vs));
      }
   }
}
