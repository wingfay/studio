using System.Collections.Specialized;


namespace Utility.HTTP
{
   public interface IHttpRequest
   {
      IResopnse Get(string URL, string Authorization);

      IResopnse Get(string URL, NameValueCollection Headers);

      IResopnse Get(string URL);

      ResopnseXML GetXML(string URL);

      ResopnseXML GetXML(string URL, string Authorization);

      IResopnse PostJSON(string URL, string Authorization, string JSON);


      IResopnse PostByFrom(string URL, string Authorization, NameValueCollection formData);

      /// <summary>
      /// add By Niko 2021-10-20 Ver 8.8.2
      /// </summary>
      /// <param name="URL"></param>
      /// <param name="Authorization"></param>
      /// <param name="Headers"></param>
      /// <param name="Body"></param>
      /// <returns></returns>
      IResopnse PostByXML(string URL, string Authorization, NameValueCollection Headers, string Body);



   }
}
