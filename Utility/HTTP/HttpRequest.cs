using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Utility.HTTP
{
   /// <summary>
   /// last modified by niko . add PostByXML method . 
   /// 2021-10-20 ILS ver 8.8.2
   /// ------------------------------------
   /// modified by niko . add GetXML method . 
   /// 2021-03-18 ILS ver 8.7.4
   /// --------------------------------
   /// Request Web Api 
   /// <Author>Niko</Author>
   /// <CreateDate>2019-2-26</CreateDate>
   /// <SystemVersion>8.4.8</SystemVersion>
   /// <Version>1.0.0</Version>
   /// </summary>
   public class HttpRequest : IHttpRequest
   {
      public Uri Uri { get; set; }

      /// <summary>
      /// Default
      /// </summary>
      public virtual Encoding Encoding { get; set; }

      /// <summary>
      /// 
      /// </summary>
      public HttpRequest()
      {
         Encoding = Encoding.Default;
      }


      public HttpRequest(Encoding encoding)
      {
         Encoding = encoding;
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
         NameValueCollection headers = new NameValueCollection();


         if (string.IsNullOrEmpty(Authorization))
         {
            headers = null;

         }
         else
         {
            headers["Authorization"] = Authorization;

         }



         return Get(URL, headers);
      }

      public IResopnse Get(string URL, NameValueCollection Headers)
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
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
         }

         try
         {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);

            if (Headers != null)
            {
               foreach (var item in Headers.AllKeys)
               {
                  request.Headers.Add(item, Headers[item]);
               }
            }


            request.Method = "GET";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
               if (response.StatusCode == HttpStatusCode.OK)
               {
                  using (var stream = response.GetResponseStream())
                  {
                     string strResult = string.Empty;
                     using (var reader = new StreamReader(stream, Encoding))
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
                     using (var streamReader = new StreamReader(stream, Encoding))
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

      public ResopnseXML GetXML(string URL, string Authorization)
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
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
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


                     return new ResopnseXML()
                     {
                        StatusCode = response.StatusCode,
                        Result = XMLUtil.ToXML(stream),

                     };
                  }
               }
               else
               {
                  return new ResopnseXML
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
                     using (var streamReader = new StreamReader(stream, Encoding))
                     {
                        XmlDocument xmlDocument = new XmlDocument();
                        xmlDocument.XmlResolver = null;
                        xmlDocument.Load(streamReader);
                        return new ResopnseXML()
                        {
                           StatusCode = response.StatusCode,
                           Result = xmlDocument,

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
         catch (XmlException xmlex)
         {
            throw xmlex;
         }
      }
      public ResopnseXML GetXML(string URL)
      {
         return GetXML(URL, string.Empty);
      }

      public IResopnse Get(string URL)
      {
         return Get(URL, string.Empty);
      }


      /// <summary>
      /// Post ，Content is Json 
      /// </summary>
      /// <param name="URL">Post Address</param>
      /// <param name="Authorization">
      /// The Authorization, if available. If not available pass null or an empty string
      /// OAuth params 
      /// Basic params,
      /// Bearer params,
      /// </param>
      /// <param name="JSON">Not Null Or Empty</param>
      /// <returns></returns>
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


         if (string.IsNullOrEmpty(JSON))
         {
            throw new ArgumentException("Param JSON Is Empty or Null!");
         }

         if (Uri.Scheme == "https")
         {
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
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
                  using (var stream = response.GetResponseStream())
                  {
                     string strResult = string.Empty;
                     using (var reader = new StreamReader(stream, Encoding))
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
                     using (var streamReader = new StreamReader(stream, Encoding))
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

      public IResopnse PostByXML(string URL, string Authorization, NameValueCollection Headers, string Body)
      {
         try
         {
            Uri = new Uri(URL);
         }
         catch (Exception ex)
         {

            throw ex;
         }


         if (string.IsNullOrEmpty(Body))
         {
            throw new ArgumentException("Param Body Is Empty or Null!");
         }

         if (Uri.Scheme == "https")
         {
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
         }

         try
         {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            if (!string.IsNullOrEmpty(Authorization))
            {
               request.Headers.Add("Authorization", Authorization);
            }

            if (Headers != null)
            {
               foreach (var item in Headers.AllKeys)
               {
                  request.Headers.Add(item, Headers[item]);
               }
            }

            request.Headers.Add("cache-control", "no-cache");
            request.ContentType = "application/xml";
            request.Method = "POST";

            // handler XML
            byte[] payload;
            payload = Encoding.UTF8.GetBytes(Body);
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
                  using (var stream = response.GetResponseStream())
                  {
                     string strResult = string.Empty;
                     using (var reader = new StreamReader(stream, Encoding))
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
                     using (var streamReader = new StreamReader(stream, Encoding))
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

         if (signatureType == SignatureTypes.HMACSHA1)
         {
            OAuthBaseSignatureTypes = OAuthBase.SignatureTypes.HMACSHA1;
         }


         string oauth_signature = oAuthBase.GenerateSignature(new Uri(url), consumerKey, consumerSecret
            , token, tokenSecret, httpMethod.ToString(), timeStamp, nonce, OAuthBaseSignatureTypes, string.Empty, out normalizedUrl, out normalizedRequestParameters);




         return oAuthBase.Header(new Uri(url), consumerKey, token, timeStamp, nonce, OAuthBaseSignatureTypes, string.Empty, oauth_signature);
      }

      #endregion


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
         return GetRandom(123400, 9999999).ToString();
      }


      private int GetRandom(int minVal, int maxVal)
      {
         int m = maxVal - minVal;
         int rnd = int.MinValue;
         decimal _base = (decimal)long.MaxValue;
         byte[] rndSeries = new byte[8];
         System.Security.Cryptography.RNGCryptoServiceProvider rng
         = new System.Security.Cryptography.RNGCryptoServiceProvider();
         rng.GetBytes(rndSeries);
         long l = BitConverter.ToInt64(rndSeries, 0);
         rnd = (int)(Math.Abs(l) / _base * m);
         return minVal + rnd;
      }

      public IResopnse PostByFrom(string URL, string Authorization, NameValueCollection postData)
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
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
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
            //request.ContentType = "application/x-www-form-urlencoded";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";


            StringBuilder stringBuilder = new StringBuilder();
            foreach (string key in postData.Keys)
            {
               stringBuilder.AppendFormat("&{0}={1}", key, postData.Get(key));
            }
            byte[] buffer = Encoding.UTF8.GetBytes(stringBuilder.ToString().Trim('&'));
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(buffer, 0, buffer.Length);
            requestStream.Close();



            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
               if (response.StatusCode == HttpStatusCode.OK
                  || response.StatusCode == HttpStatusCode.Created
                  || response.StatusCode == HttpStatusCode.Accepted)
               {
                  using (var stream = response.GetResponseStream())
                  {
                     string strResult = string.Empty;
                     using (var reader = new StreamReader(stream, Encoding))
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
                     using (var streamReader = new StreamReader(stream, Encoding))
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
   }
}
