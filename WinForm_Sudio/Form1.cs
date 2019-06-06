using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Newtonsoft.Json;
using WinForm_Sudio.AOP;
using ZK.NetStandard.Util;

namespace WinForm_Sudio
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
         // string strBool = "dsftrue234";

         //// MessageBox.Show(strBool.GetBool().ToString());

         // MessageBox.Show(strBool.GetInt().ToString());


         MessageBox.Show(toCurrency(856));
         MessageBox.Show(toCurrency(540));
         MessageBox.Show(toCurrency(100));
         MessageBox.Show(toCurrency(987));
         MessageBox.Show(toCurrency(270));
         MessageBox.Show(toCurrency(70));

         MessageBox.Show(toCurrency(77));



      }

      private string toCurrency(int Amount)
      {
         var strAmout = (Amount / 100d).ToString();

         var currency = strAmout.Split('.')[0];

         if (strAmout.IndexOf(".") > 0)
         {
            currency = strAmout.Split('.')[0];

            currency += "." + strAmout.Split('.')[1].Substring(0, strAmout.Split('.')[1].Length > 2 ? 2 : strAmout.Split('.')[1].Length);
         }
         else
         {
            currency = strAmout;
         }


         

         return currency;

      }

      private void button2_Click(object sender, EventArgs e)
        {
            BusinessHandler handler = new BusinessHandler();
            handler.DoSomething();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //List<string> a = new List<string> { "A", "N", "C" };

            //MessageBox.Show(string.Join(",", a));

            string[] arrayShortcutKey ={"","Ctrl+A","Ctrl+B"};


            List<string> Ex = new List<string> { "Ctrl+A" };

            var s= arrayShortcutKey.Except(Ex);

            MessageBox.Show(string.Join(",", s));


//            string json = @"{
//  'Name': 'Bad Boys',
//  'ReleaseDate': '1995-4-7T00:00:00',
//  'Genres': [
//    'Action',
//    'Comedy'
//  ]
//}";

//            var s = JsonConvert.DeserializeObject<Movie>(json);

//            MessageBox.Show(s.Name);



        }

        public class Movie
        {
            public string Name { get; set; }

            public DateTime ReleaseDate { get; set; }
        }

      private void button5_Click(object sender, EventArgs e)
      {
         //string s = "http://localhost/Webopac2015/onedriver/";

         //string ss = "https://www.ssss.com/ondriver/";

         //s = s.Replace("http://", "").Replace("https://", "");
         //s=s.Substring(s.IndexOf("/"));

         //ss = ss.Replace("http://", "").Replace("https://", "");
         //ss=ss.Substring(ss.IndexOf("/"));




         //MessageBox.Show("s:" + s + ",ss:" + ss);

         var s = "111".Sha1();
         var s1 = "admin".Sha1();
         var s2 = "11111111".Sha1();
         MessageBox.Show(s);


      }

      private void button6_Click(object sender, EventArgs e)
      {
         if (openFileDialog1.ShowDialog() == DialogResult.OK)
         {
            textBox1.Text = openFileDialog1.FileName;


            using (var md5 = MD5.Create())
            {
               using (var stream = File.OpenRead(openFileDialog1.FileName))
               {
                  long length = new System.IO.FileInfo(openFileDialog1.FileName).Length;
                  textBox3.Text = length.ToString();
                  var hash = md5.ComputeHash(stream);
                  textBox2.Text= BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();

               }
            }

            
         }
      }

      private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
      {
         return true; //总是接受     
      }


      private void signature(string HttpMethod,string Url,string ConsumerKey)
      {
         OAuthBase oAuthBase = new OAuthBase();
         string Time = oAuthBase.GenerateTimeStamp();
         string Nonce = oAuthBase.GenerateNonce();

         Url = System.Web.HttpUtility.HtmlEncode(Url);

         Dictionary<string, string> Params = new Dictionary<string, string>();

         Params["oauth_consumer_key"] = ConsumerKey;
         Params["oauth_signature_method"] = ConsumerKey;
         Params["oauth_consumer_key"] = ConsumerKey;

         Params["oauth_consumer_key"] = ConsumerKey;

      }

      private void button7_Click(object sender, EventArgs e)
      {
        
         try
         {
            string url = string.Format("https://api.schoology.com/v1/schools/1939634272/resources");
            Uri uri = new Uri(url);

            string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

            string ConsumerKey = "fe846419c3bbb32ad73f32f4dde1988d05c651645";
            string ConsumerSecret = "b2529f03bef043a27d938f1952f4625b";

            string JSON = @"{
                      ""title"": ""Test Search"",
                      ""type"": ""document"",
                      ""attachments"": [
                          {
                              ""url"": ""www.163.com""
                          }
                      ]
                  }";






            OAuthBase oAuthBase = new OAuthBase();
            string normalizedUrl = string.Empty;
            string normalizedRequestParameters = string.Empty;
            string Time = oAuthBase.GenerateTimeStamp();
            string nonce = oAuthBase.GenerateNonce();

            string signature =  oAuthBase.GenerateSignature(uri, ConsumerKey, ConsumerSecret, string.Empty, string.Empty, "GET", Time, nonce,OAuthBase.SignatureTypes.PLAINTEXT,string.Empty, out normalizedUrl, out normalizedRequestParameters);

            string POSTURL = string.Format("{0}?{1}&oauth_signature={2}", normalizedUrl, normalizedRequestParameters, signature);

            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            httpWebRequest.Method = "GET";
            httpWebRequest.ProtocolVersion = HttpVersion.Version11;
           

            string AuthParam = string.Format("OAuth realm=\"\",oauth_consumer_key=\"{0}\",oauth_signature_method=\"PLAINTEXT\",oauth_timestamp=\"{1}\",oauth_nonce=\"{2}\",oauth_version=\"1.0\",oauth_signature=\"{3}\""
               , ConsumerKey,Time,nonce, "b2529f03bef043a27d938f1952f4625b%26");

            httpWebRequest.Headers.Add("Authorization", AuthParam);
           // httpWebRequest.Headers.Add("cache-control", "no-cache");
            httpWebRequest.UserAgent = DefaultUserAgent;
            //HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            //httpRequestMessage.Headers.Add("Authorization", AuthParam);
            //using (var httpclient = new HttpClient())
            //{
            //   using (var resourceResponse = httpclient.SendAsync(httpRequestMessage).Result)
            //   {
            //      MessageBox.Show(resourceResponse.Content.ToString());
            //   }
            //}


            //httpWebRequest.ContentType = "application/json; charset=utf-8";

            //byte[] bytes = Encoding.UTF8.GetBytes(@"{
            //          ""title"": ""Test Search"",
            //          ""type"": ""document"",
            //          ""attachments"": [
            //              {
            //                  ""url"": ""www.163.com""
            //              }
            //          ]
            //      }");

            string strResult = string.Empty;
            httpWebRequest.KeepAlive = false;
            //httpWebRequest.Timeout = 90000;
            //httpWebRequest.ProtocolVersion = new Version(1, 1);
            ////using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream(),UTF8Encoding.UTF8))
            ////{
            ////   //streamWriter.Write(JSON);
            ////   //streamWriter.Flush();
            ////   streamWriter.Close();

            using (var response = (HttpWebResponse)httpWebRequest.GetResponse())
            {
               if(response.StatusCode == HttpStatusCode.OK)
               {
                  using (var reader = new StreamReader(response.GetResponseStream()))
                  {
                     strResult = reader.ReadToEnd();
                  }
               }
               else
               {
                  MessageBox.Show(response.StatusCode.ToString());
               }

            }


            //}

            httpWebRequest.Abort();
            MessageBox.Show(strResult);
         }
         catch (WebException ex)
         {
            string strResult = string.Empty;
            WebResponse response = ex.Response;
            if (response != null)
            {
               Stream streamReader = response.GetResponseStream();
               StreamReader sr = new StreamReader(streamReader, System.Text.Encoding.Default);
               strResult = sr.ReadToEnd();
            }
            MessageBox.Show(strResult);
         }
      }
   }

   public static class EncryptHelper
   {
      /// <summary>
      /// 基于Md5的自定义加密字符串方法：输入一个字符串，返回一个由32个字符组成的十六进制的哈希散列（字符串）。
      /// </summary>
      /// <param name="str">要加密的字符串</param>
      /// <returns>加密后的十六进制的哈希散列（字符串）</returns>
      public static string Md5(this string str)
      {
         //将输入字符串转换成字节数组
         var buffer = Encoding.Default.GetBytes(str);
         //接着，创建Md5对象进行散列计算
         var data = MD5.Create().ComputeHash(buffer);

         //创建一个新的Stringbuilder收集字节
         var sb = new StringBuilder();

         //遍历每个字节的散列数据 
         foreach (var t in data)
         {
            //格式每一个十六进制字符串
            sb.Append(t.ToString("X2"));
         }

         //返回十六进制字符串
         return sb.ToString();
      }

      /// <summary>
      /// 基于Sha1的自定义加密字符串方法：输入一个字符串，返回一个由40个字符组成的十六进制的哈希散列（字符串）。
      /// </summary>
      /// <param name="str">要加密的字符串</param>
      /// <returns>加密后的十六进制的哈希散列（字符串）</returns>
      public static string Sha1(this string str)
      {
         var buffer = Encoding.UTF8.GetBytes(str);
         var data = SHA1.Create().ComputeHash(buffer);

         var sb = new StringBuilder();
         foreach (var t in data)
         {
            sb.Append(t.ToString("X2"));
         }

         return sb.ToString();
      }
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
}
