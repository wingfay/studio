using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;

namespace TestWeb.Controllers
{
    public class ProblemController : Controller
    {
        // GET: Problem
        public ActionResult Index()
        {
            return View();
        }

      #region 超过3M的json数据从客户端post,MVC4会出现

      /// <summary>
      /// Error during serialization or deserialization using the JSON JavaScriptSerializer. 
      /// The length of the string exceeds the value set on the maxJsonLength property.Parameter name: input
      /// https://learn.microsoft.com/en-us/answers/questions/755397/maxjsonlength-error-when-decoding-jsonstring-using
      /// https://stackoverflow.com/questions/9509721/jsonvalueproviderfactory-throws-request-too-large/9512095#9512095
      /// </summary>
      /// <param name="requestContext"></param>
      /// <param name="callback"></param>
      /// <param name="state"></param>
      /// <returns></returns>
      protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
      {
         if (requestContext.RouteData.Values["action"].ToString() == "GetExportExcelReportData_Client")
         {
            StreamReader reader = new StreamReader(requestContext.HttpContext.Request.InputStream);



            string bodyText = reader.ReadToEnd();
            if (string.IsNullOrEmpty(bodyText))
            {
               // no JSON data
               return null;
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = int.MaxValue; //increase MaxJsonLength.  This could be read in from the web.config if you prefer
            object jsonData = serializer.DeserializeObject(bodyText);

            requestContext.HttpContext.Session["GetExportExcelReportData_Client"] = jsonData;


            requestContext.HttpContext.Request.InputStream.Flush();
            return base.BeginExecute(requestContext, callback, state);
         }
         else
         {
            return base.BeginExecute(requestContext, callback, state);
         }
      }


      [HttpPost]
      public ActionResult GetExportExcelReportData_Client(Dictionary<string, string> dictControlsValue)
      {

         if (this.Session["GetExportExcelReportData_Client"] != null)
         {
            var data = (Dictionary<string, object>)((Dictionary<string, object>)this.Session["GetExportExcelReportData_Client"])["dictControlsValue"];

            foreach (var item in data)
            {
               dictControlsValue[item.Key] = item.Value.ToString();

            }
         }

         //AppendBasicParams(dictControlsValue);
         //Dictionary<string, object> dictResult = (new ReportExportExcelHelper()).GetExportExcelResultData(dictControlsValue,
         //   true, Global.GetRootFolderPath("ExportFiles"), false);

         var dictResult = new Dictionary<string, string>();

         if (this.Session["GetExportExcelReportData_Client"] != null)
         {
            this.Session.Remove("GetExportExcelReportData_Client");
         }

         return LargeJson(dictResult);
      }

      public JsonResult LargeJson(object data)
      {
         JsonResult jr = Json(data);
         jr.MaxJsonLength = Int32.MaxValue;
         jr.RecursionLimit = 100;
         return jr;
      }

		#endregion
	}
}