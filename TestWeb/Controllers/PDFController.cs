using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestWeb.Controllers
{
    public class PDFController : Controller
    {
      // GET: PDF
      public ActionResult Local()
      {
         return View();
      }

      public ActionResult Stream()
      {
         return View();
      }

      public ActionResult UseDemo()
      {

         try
         {
            FileStream readPdf = new FileStream(Request.PhysicalApplicationPath + @"\customerData\BBB.pdf", FileMode.Open);
            long fileSize = readPdf.Length;
            byte[] bufferArray = new byte[fileSize];
            readPdf.Read(bufferArray, 0, (int)fileSize);
            readPdf.Close();

            string base64Str = Convert.ToBase64String(bufferArray);

            var viewModel = new PdfViewModel
            {
               PDF = base64Str
            };

            return View(viewModel);
         }
         catch (Exception ex)
         {
            return BadRequest();
         }


      }

      public ActionResult UseDemo2()
      {
         return View();
      }

      [HttpPost]
      public string getPdfStream(string args)
      {
         var returnstr = new { success = "false", message = "", data = "" };//返回值

         try
         {
            FileStream readPdf = new FileStream(Request.PhysicalApplicationPath + @"\customerData\BBB.pdf", FileMode.Open);
            long fileSize = readPdf.Length;
            byte[] bufferArray = new byte[fileSize];
            readPdf.Read(bufferArray, 0, (int)fileSize);
            readPdf.Close();

            string base64Str = Convert.ToBase64String(bufferArray);

            returnstr = new { success = "true", message = "", data = base64Str };

            return JsonConvert.SerializeObject(returnstr);
         }
         catch (Exception ex)
         {
            returnstr = new { success = "false", message = "异常信息如下：" + ex.Message, data = "" };
            return JsonConvert.SerializeObject(returnstr);
         }
      }

      protected ActionResult BadRequest()
      {
         return Redirect("~/400.html");
      }

   }

   public class PdfViewModel
   {
      public string PDF { get; set; }
   }
}