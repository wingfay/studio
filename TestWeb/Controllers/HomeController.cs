using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.XWPF.UserModel;

namespace TestWeb.Controllers
{
   public class HomeController : Controller
   {

      public byte[] CreateWordDocument()
      {
         XWPFDocument doc = new XWPFDocument();

         XWPFParagraph p1 = doc.CreateParagraph();


         XWPFRun r1 = p1.CreateRun();
         r1.SetText("The quick brown fox");
         r1.IsBold = true;
         r1.FontFamily = "Arial";
         r1.SetUnderline(UnderlinePatterns.DotDotDash);
         r1.SetTextPosition(100);

         MemoryStream memoryStream = new MemoryStream();

         StreamWriter wr = new StreamWriter(memoryStream);


         //保存文档
         doc.Write(memoryStream);

         return memoryStream.ToArray();
      }

      public byte[] CreateWordDocument2()
      {
         //创建一个Document实例并添加section
         Spire.Doc.Document doc = new Spire.Doc.Document();
         Spire.Doc.Section section = doc.AddSection();

         //添加指向网址的超链接
         Spire.Doc.Documents.Paragraph para1 = section.AddParagraph();
         para1.AppendHyperlink("www.baidu.com", "www.baidu.com",
         Spire.Doc.Documents.HyperlinkType.WebLink);

         //添加指向邮件地址的超链接
         Spire.Doc.Documents.Paragraph para2 = section.AddParagraph();
         para2.AppendHyperlink("mailto:abcd1234@qq.com", "mailto:abcd1234@qq.com",
         Spire.Doc.Documents.HyperlinkType.EMailLink);


         //设置段落之间的间距
         para1.Format.AfterSpacing = 15f;
         para2.Format.AfterSpacing = 15f;

         MemoryStream memoryStream = new MemoryStream();

         StreamWriter wr = new StreamWriter(memoryStream);


         //保存文档
         doc.SaveToStream(memoryStream, Spire.Doc.FileFormat.Docx2013);

         return memoryStream.ToArray();
      }


      public string GenerateTimeStamp()
      {
         // Default implementation of UNIX time of the current UTC time
         TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
         return Convert.ToInt64(ts.TotalSeconds).ToString();
      }



      public class Libraries
      {
         public string ID { get; set; }

         public string Title { get; set; }
      }

      /// <summary>
      /// {"id":1984211412,"title":"Test Search","type":"document","resource_notes":""
      /// ,"collection_id":1968013116,"uid":81341206,"last_updated":1551074786
      /// ,"created":1551074786,"template_fields":{"document_type":""},
      /// "folder_id":0,"attachments":{"links":{"link":[{"id":866370172,"type":"link","url":"www.163.com","title":"www.163.com","summary":"","display_inline":0}]}}}
      /// </summary>
      /// <returns></returns>
      public ActionResult Index()
      {

         //HttpRequest httpRequest = new HttpRequest();

         //string ConsumerKey = "sadf";
         //string ConsumerSecret = "b2529f03bef043a27d938f1952f4625b";


         //string GetSchool = string.Format("https://api.schoology.com/v1/schools/");


         //string OAuth = httpRequest.BuildOAuth(GetSchool, ConsumerKey, ConsumerSecret, HttpMethod.POST, SignatureTypes.PLAINTEXT);

         //var resopnse =  httpRequest.Get(GetSchool, OAuth);

         //List<Libraries> libraries = new List<Libraries>();
         //if (resopnse.StatusCode == HttpStatusCode.OK)
         //{
         //   var oResult = JsonConvert.DeserializeObject<JObject>(resopnse.Result);

         //   var s = oResult.Children();

         //   foreach(var i in   oResult["school"].Children())
         //   {
         //      libraries.Add(new Libraries
         //      {
         //         ID = i["id"].ToString(),
         //         Title = i["title"].ToString(),
         //      });
         //   }
         //}

         //string url = string.Format("https://api.schoology.com/v1/schools/{0}/resources", libraries[0].ID);

         //string JSON = @"{
         //             ""title"": ""Item Date Published Test 01 : site 0001"",
         //             ""type"": ""document"",
         //             ""attachments"": [
         //                 {
         //                     ""url"": ""http://localhost/Webopac2015/ItemDetail?l=All&amp;i=151847""
         //                 }
         //             ]
         //         }";


         //OAuth = httpRequest.BuildOAuth(url, ConsumerKey, ConsumerSecret, HttpMethod.POST, SignatureTypes.PLAINTEXT);


         //string strResult = string.Empty;

         //resopnse = httpRequest.PostJSON(url, OAuth, JSON);

         //if(resopnse.StatusCode == HttpStatusCode.Created)
         //{
         //   strResult = resopnse.Result;
         //}


         if (Session["key"] == null)
         {
            Session["key"]= Guid.NewGuid().ToString("N");
         }

         return View();
      }


      private void OldCode()
      {
         string url = string.Format("https://api.schoology.com/v1/schools/1939634272/resources");

         string ConsumerKey = "fe846419c3bbb32ad73f32f4dde1988d05c651645";
         string ConsumerSecret = "b2529f03bef043a27d938f1952f4625b";

         ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;
         ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
         HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

         string JSON = @"{
                      ""title"": ""Test Search"",
                      ""type"": ""document"",
                      ""attachments"": [
                          {
                              ""url"": ""www.163.com""
                          }
                      ]
                  }";

         httpWebRequest.Method = "POST";

         string AuthParam = string.Format("OAuth realm=\"\",oauth_consumer_key=\"{0}\",oauth_signature_method=\"PLAINTEXT\",oauth_timestamp=\"{1}\",oauth_nonce=\"{2}\",oauth_version=\"1.0\",oauth_signature=\"{3}\""
   , ConsumerKey, GenerateTimeStamp(), Guid.NewGuid().ToString(), "b2529f03bef043a27d938f1952f4625b%26");

         httpWebRequest.Headers.Add("Authorization", AuthParam);
         httpWebRequest.Headers.Add("cache-control", "no-cache");
         httpWebRequest.ContentType = "application/json";


         byte[] payload;
         payload = System.Text.Encoding.UTF8.GetBytes(JSON);
         httpWebRequest.ContentLength = payload.Length;
         Stream writer = httpWebRequest.GetRequestStream();
         writer.Write(payload, 0, payload.Length);
         writer.Close();


         using (var response = (HttpWebResponse)httpWebRequest.GetResponse())
         {
            var s = response.StatusCode.ToString();
            if (response.StatusCode == HttpStatusCode.Created)
            {
               using (var reader = new StreamReader(response.GetResponseStream()))
               {
                  string strResult = reader.ReadToEnd();
               }
            }

         }
      }

      [AcceptVerbs(HttpVerbs.Get)]
      public FileResult GetDoc()
      {

         var byteArray = CreateWordDocument2();
         //var byteArray = example();
         Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
         return File(byteArray, "application/ms-word", "wordtest" + ".docx");
      }



      public ActionResult About()
      {
         string ondDriverHTML = string.Format(@"
    <script type = 'text/javascript' >
      function launchSaveToOneDrive() {{
         $.getJSON('{4}GetBase64',{{ id: {5} }}, function (json) {{
        var odOptions = {{
                clientId: ""{1}"",
                action: ""save"",
                sourceInputElementId: """",
                sourceUri: ""{2}"",
                fileName: ""http.doc"",
                openInNewWindow: true,
               advanced: {{
                   redirectUri: ""{3}""
                 }},
                success: function(files) {{ Util.alert(""success""); }},
                progress: function(p) {{ /* progress handler */ }},
                cancel: function() {{ Util.alert(""cancel""); }},
                error: function(e) {{ Util.alert("""",e); }}
         }}
         OneDrive.save(odOptions);
      }}

   </script>
   <a href='#' onclick=launchSaveToOneDrive()><img src='{0}/system/OneDrive_24px.png' /></a>
      ", "1", "2", "2", "2", "2", "2");
         ViewBag.Message = "Your application description page.";
         ViewBag.d = ondDriverHTML;
         return View();
      }



      public JsonResult GetBase64(int id)
      {
         var byteArray = CreateWordDocument2();
         var Base64 = Convert.ToBase64String(byteArray);
         return Json("data:application/ms-word;base64,0M8R4KGxGuEAAAAAAAAAAAAAAAAAAAAAPgADAP7/CQAGAAAAAAAAAAAAAAABAAAAAQAAAAAAAAAAEAAAAgAAAAEAAAD+////AAAAAAAAAAD////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////9////DQAAAP7///8EAAAABQAAAAYAAAAHAAAACAAAAAkAAAAKAAAACwAAAAwAAAAOAAAA/v////7/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////…", JsonRequestBehavior.AllowGet);
      }

      [HttpPost]
      public ActionResult EncryptByAES(string EncryptData)
      {

         Session["AESSource"] = EncryptData;
         Session["AES"] = AESHelper.EncryptByAES(EncryptData, Session["Key"].ToString());
         return Redirect("index");
      }

      [HttpPost]
      public ActionResult DecryptByAES(string DecryptData)
      {

         Session["DecryptSource"] = DecryptData;
         Session["Decrypt"] = AESHelper.DecryptByAES(DecryptData, Session["Key"].ToString());
         return Redirect("index");
      }

      public ActionResult Demo()
      {
         return View();
      }
   }





   #region auth

   public interface IHttpRequest
   {
      IResopnse GetResopnse();
   }

   public class HttpRequest : IHttpRequest
   {
      private Random random;

      public Uri Uri { get; set; }

      /// <summary>
      /// Default Utf8
      /// </summary>
      public virtual Encoding Encoding { get; set; }


    

      /// <summary>
      /// 
      /// </summary>
      public HttpRequest()
      {
         random = new Random();
         Encoding = Encoding.UTF8;
      }
      

      private bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
      {
         return true;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="URL"></param>
      /// <param name="Authorization">
      /// The Authorization, if available. If not available pass null or an empty string
      /// OAuth params 
      /// Basic params,
      /// Bearer params,
      /// </param>
      /// <returns></returns>
      public IResopnse Get(string URL, string Authorization)
      {

         try
         {
            Uri = new Uri(URL);
         }
         catch (Exception ex)
         {

            throw ex;
         }

         if (Uri.Scheme == "https")
         {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
         }

         try
         {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            if (!string.IsNullOrEmpty(Authorization))
            {
               request.Headers.Add("Authorization", Authorization);
            }
            request.Method = "GET";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
               if (response.StatusCode == HttpStatusCode.OK)
               {
                  using (var stream = response.GetResponseStream())
                  {
                     string strResult = string.Empty;
                     using (var reader = new StreamReader(stream, Encoding.Default))
                     {
                        strResult = reader.ReadToEnd();
                     }

                     return new Resopnse()
                     {
                        StatusCode = response.StatusCode,
                        Result = strResult,

                     };
                  }
               }
               else
               {
                  return new Resopnse
                  {
                     StatusCode = response.StatusCode,
                     Result = null,
                  };
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
                     using (var streamReader = new StreamReader(stream, System.Text.Encoding.Default))
                     {
                        return new Resopnse()
                        {
                           StatusCode = response.StatusCode,
                           Result = streamReader.ReadToEnd(),

                        };
                     }
                  }
               }
            }
            throw ex;
         }
      }

      public IResopnse PostJSON(string URL, string Authorization, string JSON)
      {
         try
         {
            Uri = new Uri(URL);
         }
         catch (Exception ex)
         {

            throw ex;
         }

         if (Uri.Scheme == "https")
         {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
         }

         try
         {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            if (!string.IsNullOrEmpty(Authorization))
            {
               request.Headers.Add("Authorization", Authorization);
            }

            request.Headers.Add("cache-control", "no-cache");
            request.ContentType = "application/json";
            request.Method = "POST";

            // handler JSON
            byte[] payload;
            payload = Encoding.UTF8.GetBytes(JSON);
            request.ContentLength = payload.Length;
            Stream writer = request.GetRequestStream();
            writer.Write(payload, 0, payload.Length);
            writer.Close();

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
               if (response.StatusCode == HttpStatusCode.OK
                  || response.StatusCode == HttpStatusCode.Created
                  || response.StatusCode == HttpStatusCode.Accepted)
               {
                  using (var stream =  response.GetResponseStream())
                  {
                     string strResult = string.Empty;
                     using (var reader = new StreamReader(stream, Encoding.Default))
                     {
                        strResult = reader.ReadToEnd();
                     }

                     return new Resopnse()
                     {
                        StatusCode = response.StatusCode,
                        Result = strResult,

                     };
                  }
               }
               else
               {
                  return new Resopnse
                  {
                     StatusCode = response.StatusCode,
                     Result = null,
                  };
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
                     using (var streamReader = new StreamReader(stream, System.Text.Encoding.Default))
                     {
                        return new Resopnse()
                        {
                           StatusCode = response.StatusCode,
                           Result = streamReader.ReadToEnd(),

                        };
                     }
                  }
               }
            }
            throw ex;
         }
         catch (ProtocolViolationException protocolviolationexception)
         {
            throw protocolviolationexception;
         }
         catch (InvalidOperationException invalidoperationexception)
         {
            throw invalidoperationexception;
         }
         catch (NotSupportedException notsupportedexception)
         {
            throw notsupportedexception;
         }

      }



      #region Buld Oauth Param

      /// <summary>
      /// Generates a signature using the specified signatureType 
      /// </summary>        
      /// <param name="url">The full url that needs to be signed including its non OAuth url parameters</param>
      /// <param name="consumerKey">The consumer key</param>
      /// <param name="consumerSecret">The consumer seceret</param>
      /// <param name="token">The token, if available. If not available pass null or an empty string</param>
      /// <param name="tokenSecret">The token secret, if available. If not available pass null or an empty string</param>
      /// <param name="httpMethod">The http method used. Must be a valid HTTP method verb (POST,GET,PUT, etc)</param>
      /// <param name="signatureType">The type of signature to use</param>
      /// <returns>A base64 string of the hash value</returns>
      public string BuildOAuth(string url, string consumerKey, string consumerSecret, HttpMethod httpMethod, SignatureTypes signatureTypes)
      {

         return BuildOAuth(url, consumerKey, consumerSecret, string.Empty, string.Empty, httpMethod, GenerateTimeStamp(), GenerateNonce(), signatureTypes);
      }

      /// <summary>
      /// Generates a signature using the specified signatureType 
      /// </summary>        
      /// <param name="url">The full url that needs to be signed including its non OAuth url parameters</param>
      /// <param name="consumerKey">The consumer key</param>
      /// <param name="consumerSecret">The consumer seceret</param>
      /// <param name="token">The token, if available. If not available pass null or an empty string</param>
      /// <param name="tokenSecret">The token secret, if available. If not available pass null or an empty string</param>
      /// <param name="httpMethod">The http method used. Must be a valid HTTP method verb (POST,GET,PUT, etc)</param>
      /// <param name="signatureType">The type of signature to use</param>
      /// <returns>A base64 string of the hash value</returns>
      public string BuildOAuth(string url, string consumerKey, string consumerSecret, string token, string tokenSecret, HttpMethod httpMethod
         , string timeStamp, string nonce, SignatureTypes signatureType)
      {
         OAuthBase oAuthBase = new OAuthBase();
         string normalizedUrl = string.Empty;
         string normalizedRequestParameters = string.Empty;

         OAuthBase.SignatureTypes OAuthBaseSignatureTypes = OAuthBase.SignatureTypes.PLAINTEXT;

         if(signatureType == SignatureTypes.HMACSHA1)
         {
            OAuthBaseSignatureTypes = OAuthBase.SignatureTypes.HMACSHA1;
         }


         string oauth_signature = oAuthBase.GenerateSignature(new Uri(url), consumerKey, consumerSecret
            , token, tokenSecret, httpMethod.ToString(), timeStamp, nonce, OAuthBaseSignatureTypes,string.Empty, out normalizedUrl, out normalizedRequestParameters);




         return oAuthBase.Header(new Uri(url), consumerKey, token, timeStamp, nonce, OAuthBaseSignatureTypes, string.Empty, oauth_signature);
      }

      #endregion



      public IResopnse GetResopnse()
      {
         return new Resopnse();
      }

      /// <summary>
      /// Generate the timestamp for the signature        
      /// </summary>
      /// <returns></returns>
      public virtual string GenerateTimeStamp()
      {
         // Default implementation of UNIX time of the current UTC time
         TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
         return Convert.ToInt64(ts.TotalSeconds).ToString();
      }

      /// <summary>
      /// Generate a nonce
      /// Use GUID
      /// </summary>
      /// <returns></returns>
      public virtual string GenerateNonce()
      {
         // Just a simple implementation of a random number between 123400 and 9999999
         return Guid.NewGuid().ToString();
      }

      /// <summary>
      /// Generate a nonce
      /// Use GUID
      /// </summary>
      /// <returns></returns>
      public virtual string GenerateNonceRandom()
      {
         // Just a simple implementation of a random number between 123400 and 9999999
         return random.Next(123400, 9999999).ToString();
      }


   }



   public enum HttpMethod
   {
      GET,
      POST,
      DELETE,

   }

   public interface IResopnse
   {
      HttpStatusCode StatusCode { get; set; }
      string Result { get; set; }


   }


   public class Resopnse : IResopnse
   {
      public HttpStatusCode StatusCode { get; set; }
      public string Result { get; set; }
   }

   /// <summary>
   /// 
   /// </summary>
   public enum HttpRequestAuthorization
   {
      OAuth,
      Basic,
      Bearer,

   }

   /// <summary>
   /// Oauth Signature
   /// </summary>
   public enum SignatureTypes
   {
      HMACSHA1,
      HMACSHA256,
      PLAINTEXT,

   }

   public class OAuthBase
   {

      /// <summary>
      /// Provides a predefined set of algorithms that are supported officially by the protocol
      /// </summary>
      public enum SignatureTypes
      {
         HMACSHA1,
         PLAINTEXT,
         RSASHA1
      }

      /// <summary>
      /// Provides an internal structure to sort the query parameter
      /// </summary>
      protected class QueryParameter
      {
         public QueryParameter()
         {
            Name = null;
            Value = null;
         }

         public QueryParameter(string name, string value)
         {
            Name = name;
            Value = value;
         }

         public string Name
         {
            get;
            private set;
         }

         public string Value
         {
            get;
            private set;
         }
      }



      protected const string OAuthVersion = "1.0";
      protected const string OAuthParameterPrefix = "oauth_";

      //
      // List of know and used oauth parameters' names
      //        
      protected const string OAuthConsumerKeyKey = "oauth_consumer_key";
      protected const string OAuthCallbackKey = "oauth_callback";
      protected const string OAuthVersionKey = "oauth_version";
      protected const string OAuthSignatureMethodKey = "oauth_signature_method";
      protected const string OAuthSignatureKey = "oauth_signature";
      protected const string OAuthTimestampKey = "oauth_timestamp";
      protected const string OAuthNonceKey = "oauth_nonce";
      protected const string OAuthTokenKey = "oauth_token";
      protected const string OAuthVerifier = "oauth_verifier";
      protected const string OAuthTokenSecretKey = "oauth_token_secret";

      protected const string HMACSHA1SignatureType = "HMAC-SHA1";
      protected const string PlainTextSignatureType = "PLAINTEXT";
      protected const string RSASHA1SignatureType = "RSA-SHA1";

      protected Random random = new Random();

      protected string unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

      /// <summary>
      /// Helper function to compute a hash value
      /// </summary>
      /// <param name="hashAlgorithm">The hashing algoirhtm used. If that algorithm needs some initialization, like HMAC and its derivatives, they should be initialized prior to passing it to this function</param>
      /// <param name="data">The data to hash</param>
      /// <returns>a Base64 string of the hash value</returns>
      private string ComputeHash(HashAlgorithm hashAlgorithm, string data)
      {
         if (hashAlgorithm == null)
         {
            throw new ArgumentNullException("hashAlgorithm");
         }

         if (string.IsNullOrEmpty(data))
         {
            throw new ArgumentNullException("data");
         }

         byte[] dataBuffer = System.Text.Encoding.UTF8.GetBytes(data);
         byte[] hashBytes = hashAlgorithm.ComputeHash(dataBuffer);

         return Convert.ToBase64String(hashBytes);
      }

      /// <summary>
      /// Internal function to cut out all non oauth query string parameters (all parameters not begining with "oauth_")
      /// </summary>
      /// <param name="parameters">The query string part of the Url</param>
      /// <returns>A list of QueryParameter each containing the parameter name and value</returns>
      private List<QueryParameter> GetQueryParameters(string parameters)
      {
         if (parameters.StartsWith("?"))
         {
            parameters = parameters.Remove(0, 1);
         }

         List<QueryParameter> result = new List<QueryParameter>();

         if (!string.IsNullOrEmpty(parameters))
         {
            string[] p = parameters.Split('&');
            foreach (string s in p)
            {
               if (!string.IsNullOrEmpty(s) && !s.StartsWith(OAuthParameterPrefix))
               {
                  if (s.IndexOf('=') > -1)
                  {
                     string[] temp = s.Split('=');
                     result.Add(new QueryParameter(temp[0], temp[1]));
                  }
                  else
                  {
                     result.Add(new QueryParameter(s, string.Empty));
                  }
               }
            }
         }

         return result;
      }

      /// <summary>
      /// This is a different Url Encode implementation since the default .NET one outputs the percent encoding in lower case.
      /// While this is not a problem with the percent encoding spec, it is used in upper case throughout OAuth
      /// </summary>
      /// <param name="value">The value to Url encode</param>
      /// <returns>Returns a Url encoded string</returns>
      protected string UrlEncode(string value)
      {
         StringBuilder result = new StringBuilder();

         foreach (char symbol in value)
         {
            if (unreservedChars.IndexOf(symbol) != -1)
            {
               result.Append(symbol);
            }
            else
            {
               result.Append('%' + string.Format("{0:X2}", (int)symbol));
            }
         }

         return result.ToString();
      }

      /// <summary>
      /// Normalizes the request parameters according to the spec
      /// </summary>
      /// <param name="parameters">The list of parameters already sorted</param>
      /// <returns>a string representing the normalized parameters</returns>
      protected string NormalizeRequestParameters(IList<QueryParameter> parameters)
      {
         StringBuilder sb = new StringBuilder();
         QueryParameter p = null;
         for (int i = 0; i < parameters.Count; i++)
         {
            p = parameters[i];
            sb.AppendFormat("{0}={1}", p.Name, p.Value);

            if (i < parameters.Count - 1)
            {
               sb.Append("&");
            }
         }

         return sb.ToString();
      }

      /// <summary>
      /// Generate the signature base that is used to produce the signature
      /// </summary>
      /// <param name="url">The full url that needs to be signed including its non OAuth url parameters</param>
      /// <param name="consumerKey">The consumer key</param>        
      /// <param name="token">The token, if available. If not available pass null or an empty string</param>
      /// <param name="tokenSecret">The token secret, if available. If not available pass null or an empty string</param>
      /// <param name="httpMethod">The http method used. Must be a valid HTTP method verb (POST,GET,PUT, etc)</param>
      /// <param name="signatureType">The signature type. To use the default values use <see cref="OAuthBase.SignatureTypes">OAuthBase.SignatureTypes</see>.</param>
      /// <returns>The signature base</returns>
      public string GenerateSignatureBase(Uri url, string consumerKey, string token, string tokenSecret, string httpMethod, string timeStamp, string nonce, string signatureType, string verifier, out string normalizedUrl, out string normalizedRequestParameters)
      {
         if (token == null)
         {
            token = string.Empty;
         }

         if (tokenSecret == null)
         {
            tokenSecret = string.Empty;
         }

         if (string.IsNullOrEmpty(consumerKey))
         {
            throw new ArgumentNullException("consumerKey");
         }

         if (string.IsNullOrEmpty(httpMethod))
         {
            throw new ArgumentNullException("httpMethod");
         }

         if (string.IsNullOrEmpty(signatureType))
         {
            throw new ArgumentNullException("signatureType");
         }

         normalizedUrl = null;
         normalizedRequestParameters = null;

         List<QueryParameter> parameters = GetQueryParameters(url.Query);
         parameters.Add(new QueryParameter(OAuthVersionKey, OAuthVersion));
         parameters.Add(new QueryParameter(OAuthNonceKey, nonce));
         parameters.Add(new QueryParameter(OAuthTimestampKey, timeStamp));
         parameters.Add(new QueryParameter(OAuthSignatureMethodKey, signatureType));
         parameters.Add(new QueryParameter(OAuthConsumerKeyKey, consumerKey));

         if (!string.IsNullOrEmpty(token))
         {
            parameters.Add(new QueryParameter(OAuthTokenKey, token));
         }

         if (!string.IsNullOrEmpty(verifier))
         {
            parameters.Add(new QueryParameter(OAuthVerifier, verifier));
         }

         parameters = parameters.OrderBy(d => d.Name).OrderBy(d => d.Value).ToList();

         normalizedUrl = string.Format("{0}://{1}", url.Scheme, url.Host);
         if (!((url.Scheme == "http" && url.Port == 80) || (url.Scheme == "https" && url.Port == 443)))
         {
            normalizedUrl += ":" + url.Port;
         }
         normalizedUrl += url.AbsolutePath;
         normalizedRequestParameters = NormalizeRequestParameters(parameters);

         StringBuilder signatureBase = new StringBuilder();
         signatureBase.AppendFormat("{0}&", httpMethod.ToUpper());
         signatureBase.AppendFormat("{0}&", UrlEncode(normalizedUrl));
         signatureBase.AppendFormat("{0}", UrlEncode(normalizedRequestParameters));

         return signatureBase.ToString();
      }


      public string Header(Uri url, string consumerKey, string token, string timeStamp, string nonce, SignatureTypes signatureType, string verifier,string signature)
      {
         List<QueryParameter> parameters = GetQueryParameters(url.Query);
         parameters.Add(new QueryParameter(OAuthVersionKey, OAuthVersion));
         parameters.Add(new QueryParameter(OAuthNonceKey, nonce));
         parameters.Add(new QueryParameter(OAuthTimestampKey, timeStamp));
         switch (signatureType)
         {
            case SignatureTypes.HMACSHA1:
               parameters.Add(new QueryParameter(OAuthSignatureMethodKey, HMACSHA1SignatureType));
               break;
            case SignatureTypes.PLAINTEXT:
               parameters.Add(new QueryParameter(OAuthSignatureMethodKey, PlainTextSignatureType));
               break;
            case SignatureTypes.RSASHA1:
               parameters.Add(new QueryParameter(OAuthSignatureMethodKey, RSASHA1SignatureType));
               break;
            default:
               break;
         }
        
         parameters.Add(new QueryParameter(OAuthConsumerKeyKey, consumerKey));

         if (!string.IsNullOrEmpty(token))
         {
            parameters.Add(new QueryParameter(OAuthTokenKey, token));
         }

         if (!string.IsNullOrEmpty(verifier))
         {
            parameters.Add(new QueryParameter(OAuthVerifier, verifier));
         }

         parameters = parameters.OrderBy(d => d.Name).OrderBy(d => d.Value).ToList();

         parameters.Add(new QueryParameter(OAuthSignatureKey, signature));

         StringBuilder AuthParam = new StringBuilder();

         AuthParam.Append("OAuth ");
         AuthParam.Append("realm=\"\",");
         foreach (var p in parameters)
         {
            AuthParam.AppendFormat("{0}=\"{1}\",", p.Name, p.Value);
         }


         return AuthParam.ToString(0, AuthParam.Length - 1);


      }

      /// <summary>
      /// Generate the signature value based on the given signature base and hash algorithm
      /// </summary>
      /// <param name="signatureBase">The signature based as produced by the GenerateSignatureBase method or by any other means</param>
      /// <param name="hash">The hash algorithm used to perform the hashing. If the hashing algorithm requires initialization or a key it should be set prior to calling this method</param>
      /// <returns>A base64 string of the hash value</returns>
      public string GenerateSignatureUsingHash(string signatureBase, HashAlgorithm hash)
      {
         return ComputeHash(hash, signatureBase);
      }

      /// <summary>
      /// Generates a signature using the HMAC-SHA1 algorithm
      /// </summary>        
      /// <param name="url">The full url that needs to be signed including its non OAuth url parameters</param>
      /// <param name="consumerKey">The consumer key</param>
      /// <param name="consumerSecret">The consumer seceret</param>
      /// <param name="token">The token, if available. If not available pass null or an empty string</param>
      /// <param name="tokenSecret">The token secret, if available. If not available pass null or an empty string</param>
      /// <param name="httpMethod">The http method used. Must be a valid HTTP method verb (POST,GET,PUT, etc)</param>
      /// <returns>A base64 string of the hash value</returns>
      public string GenerateSignature(Uri url, string consumerKey, string consumerSecret, string token, string tokenSecret, string httpMethod, string timeStamp, string nonce, string verifier, out string normalizedUrl, out string normalizedRequestParameters)
      {
         return GenerateSignature(url, consumerKey, consumerSecret, token, tokenSecret, httpMethod, timeStamp, nonce, SignatureTypes.HMACSHA1, verifier, out normalizedUrl, out normalizedRequestParameters);
      }

      /// <summary>
      /// Generates a signature using the specified signatureType 
      /// </summary>        
      /// <param name="url">The full url that needs to be signed including its non OAuth url parameters</param>
      /// <param name="consumerKey">The consumer key</param>
      /// <param name="consumerSecret">The consumer seceret</param>
      /// <param name="token">The token, if available. If not available pass null or an empty string</param>
      /// <param name="tokenSecret">The token secret, if available. If not available pass null or an empty string</param>
      /// <param name="httpMethod">The http method used. Must be a valid HTTP method verb (POST,GET,PUT, etc)</param>
      /// <param name="signatureType">The type of signature to use</param>
      /// <returns>A base64 string of the hash value</returns>
      public string GenerateSignature(Uri url, string consumerKey, string consumerSecret, string token, string tokenSecret, string httpMethod, string timeStamp, string nonce, SignatureTypes signatureType, string verifier, out string normalizedUrl, out string normalizedRequestParameters)
      {
         normalizedUrl = null;
         normalizedRequestParameters = null;

         switch (signatureType)
         {
            case SignatureTypes.PLAINTEXT:
               return HttpUtility.UrlEncode(string.Format("{0}&{1}", consumerSecret, tokenSecret));
            case SignatureTypes.HMACSHA1:
               string signatureBase = GenerateSignatureBase(url, consumerKey, token, tokenSecret, httpMethod, timeStamp, nonce, HMACSHA1SignatureType, verifier, out normalizedUrl, out normalizedRequestParameters);

               HMACSHA1 hmacsha1 = new HMACSHA1();
               hmacsha1.Key = Encoding.UTF8.GetBytes(string.Format("{0}&{1}", UrlEncode(consumerSecret), string.IsNullOrEmpty(tokenSecret) ? "" : UrlEncode(tokenSecret)));

               return GenerateSignatureUsingHash(signatureBase, hmacsha1);
            case SignatureTypes.RSASHA1:
               throw new NotImplementedException();
            default:
               throw new ArgumentException("Unknown signature type", "signatureType");
         }
      }

      /// <summary>
      /// Generate the timestamp for the signature        
      /// </summary>
      /// <returns></returns>
      public virtual string GenerateTimeStamp()
      {
         // Default implementation of UNIX time of the current UTC time
         TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
         return Convert.ToInt64(ts.TotalSeconds).ToString();
      }

      /// <summary>
      /// Generate a nonce
      /// </summary>
      /// <returns></returns>
      public virtual string GenerateNonce()
      {
         // Just a simple implementation of a random number between 123400 and 9999999
         return Guid.NewGuid().ToString();
      }

   }

   #endregion

   #region AES

   public class AESHelper
   {

      
      const string AES_IV = "1234567890000000";//16位    

      /// <summary>  
      /// AES加密算法  
      /// </summary>  
      /// <param name="input">明文字符串</param>  
      /// <param name="key">密钥（32位）</param>  
      /// <returns>字符串</returns>  
      public static string EncryptByAES(string input, string key)
      {
         byte[] keyBytes = Encoding.UTF8.GetBytes(key.Substring(0, 32));
         using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
         {
            aesAlg.Key = keyBytes;
            aesAlg.IV = Encoding.UTF8.GetBytes(AES_IV.Substring(0, 16));

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            using (MemoryStream msEncrypt = new MemoryStream())
            {
               using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
               {
                  using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                  {
                     swEncrypt.Write(input);
                  }
                  byte[] bytes = msEncrypt.ToArray();
                  return ByteArrayToHexString(bytes);
               }
            }
         }
      }

      /// <summary>  
      /// AES解密  
      /// </summary>  
      /// <param name="input">密文字节数组</param>  
      /// <param name="key">密钥（32位）</param>  
      /// <returns>返回解密后的字符串</returns>  
      public static string DecryptByAES(string input, string key)
      {
         byte[] inputBytes = HexStringToByteArray(input);
         byte[] keyBytes = Encoding.UTF8.GetBytes(key.Substring(0, 32));
         using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
         {
            aesAlg.Key = keyBytes;
            aesAlg.IV = Encoding.UTF8.GetBytes(AES_IV.Substring(0, 16));

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            using (MemoryStream msEncrypt = new MemoryStream(inputBytes))
            {
               using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, decryptor, CryptoStreamMode.Read))
               {
                  using (StreamReader srEncrypt = new StreamReader(csEncrypt))
                  {
                     return srEncrypt.ReadToEnd();
                  }
               }
            }
         }
      }

      /// <summary>
      /// 将指定的16进制字符串转换为byte数组
      /// </summary>
      /// <param name="s">16进制字符串(如：“7F 2C 4A”或“7F2C4A”都可以)</param>
      /// <returns>16进制字符串对应的byte数组</returns>
      public static byte[] HexStringToByteArray(string s)
      {
         s = s.Replace(" ", "");
         byte[] buffer = new byte[s.Length / 2];
         for (int i = 0; i < s.Length; i += 2)
            buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
         return buffer;
      }

      /// <summary>
      /// 将一个byte数组转换成一个格式化的16进制字符串
      /// </summary>
      /// <param name="data">byte数组</param>
      /// <returns>格式化的16进制字符串</returns>
      public static string ByteArrayToHexString(byte[] data)
      {
         StringBuilder sb = new StringBuilder(data.Length * 3);
         foreach (byte b in data)
         {
            //16进制数字
            sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            //16进制数字之间以空格隔开
            //sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
         }
         return sb.ToString();
      }
   }

   #endregion



}