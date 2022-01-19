using Google.Apis.Services;
using System;
using System.Threading.Tasks;

namespace SSODEMO
{
   class Program
   {
      [STAThread]
      static void Main(string[] args)
      {
         Console.WriteLine("Discovery API Sample");
         Console.WriteLine("====================");
         try
         {
            new Program().Run().Wait();
         }
         catch (AggregateException ex)
         {
            foreach (var e in ex.InnerExceptions)
            {
               Console.WriteLine("ERROR: " + e.Message);
            }
         }
         Console.WriteLine("Press any key to continue...");
         Console.ReadKey();
      }

      private async Task Run()
      {
         // Create the service.
         var service = new DiscoveryService(new BaseClientService.Initializer
         {
            ApplicationName = "Discovery Sample",
            ApiKey = "[YOUR_API_KEY_HERE]",
         });

         // Run the request.
         Console.WriteLine("Executing a list request...");
         var result = await service.Apis.List().ExecuteAsync();

         // Display the results.
         if (result.Items != null)
         {
            foreach (DirectoryList.ItemsData api in result.Items)
            {
               Console.WriteLine(api.Id + " - " + api.Title);
            }
         }
      }



      public class GooglePlusAccessToken
      {
         public string access_token { get; set; }
         public string token_type { get; set; }
         public int expires_in { get; set; }
         public string id_token { get; set; }
         public string refresh_token { get; set; }
      }

      protected string googleplus_client_id = "973553899559-dsdeo5mkhj4an1i4lu1j8tm4nl8nofeo.apps.googleusercontent.com";    // Replace this with your Client ID
      protected string googleplus_client_secret = "4hiVJYlomswRd_PV5lyNQlfN";                                                // Replace this with your Client Secret
      protected string googleplus_redirect_url = "http://localhost/ILSWeb2015/Login/GoogleRedirect";                                         // Replace this with your Redirect URL; Your Redirect URL from your developer.google application should match this URL.
      protected string Parameters;

      protected void Google_Click()
      {
         if ((Session["loginWith"] == null))
         {
            var Googleurl = "https://accounts.google.com/o/oauth2/auth?response_type=code&redirect_uri=" + googleplus_redirect_url + "&scope=https://www.googleapis.com/auth/userinfo.email%20https://www.googleapis.com/auth/userinfo.profile&client_id=" + googleplus_client_id;
            Session["loginWith"] = "google";
            Response.Redirect(Googleurl);
         }
         else
         {
            GetGoogleInfo();
         }

      }



      protected void GetGoogleInfo()
      {
         try
         {
            if ((Session.Contents.Count > 0) && (Session["loginWith"] != null) && (Session["loginWith"].ToString() == "google"))
            {
               var url = Request.Url.Query;
               if (url != "")
               {
                  string queryString = url.ToString();
                  char[] delimiterChars = { '=' };
                  string[] words = queryString.Split(delimiterChars);
                  string code = words[1];

                  if (code != null)
                  {


                     //get the access token
                     HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://accounts.google.com/o/oauth2/token");
                     webRequest.Method = "POST";
                     Parameters = "code=" + code + "&client_id=" + googleplus_client_id + "&client_secret=" + googleplus_client_secret + "&redirect_uri=" + googleplus_redirect_url + "&grant_type=authorization_code";
                     byte[] byteArray = Encoding.UTF8.GetBytes(Parameters);
                     webRequest.ContentType = "application/x-www-form-urlencoded";
                     webRequest.ContentLength = byteArray.Length;
                     Stream postStream = webRequest.GetRequestStream();
                     // Add the post data to the web request
                     postStream.Write(byteArray, 0, byteArray.Length);
                     postStream.Close();

                     WebResponse response = webRequest.GetResponse();
                     postStream = response.GetResponseStream();
                     StreamReader reader = new StreamReader(postStream);
                     string responseFromServer = reader.ReadToEnd();

                     GooglePlusAccessToken serStatus = JsonConvert.DeserializeObject<GooglePlusAccessToken>(responseFromServer);

                     if (serStatus != null)
                     {
                        string accessToken = string.Empty;
                        accessToken = serStatus.access_token;

                        if (!string.IsNullOrEmpty(accessToken))
                        {
                           // This is where you want to add the code if login is successful.
                           getgoogleplususerdataSer(accessToken);
                        }
                     }

                  }
               }
            }

         }
         catch (Exception ex)
         {
            throw ex;
            //throw new Exception(ex.Message, ex);
            //Response.Redirect("index.aspx");
         }

      }



      private async void getgoogleplususerdataSer(string access_token)
      {
         try
         {
            HttpClient client = new HttpClient();
            var urlProfile = "https://www.googleapis.com/oauth2/v1/userinfo?access_token=" + access_token;

            client.CancelPendingRequests();
            HttpResponseMessage output = await client.GetAsync(urlProfile);

            if (output.IsSuccessStatusCode)
            {
               string outputData = await output.Content.ReadAsStringAsync();
               GoogleUserOutputData serStatus = JsonConvert.DeserializeObject<GoogleUserOutputData>(outputData);

               if (serStatus != null)
               {
                  // You will get the user information here.
               }
            }
         }
         catch (Exception ex)
         {
            //catching the exception
         }
      }

      public class GoogleUserOutputData
      {
         public string id { get; set; }
         public string name { get; set; }
         public string given_name { get; set; }
         public string email { get; set; }
         public string picture { get; set; }
      }

   }
}
