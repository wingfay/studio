using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
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
         doc.SaveToStream(memoryStream,Spire.Doc.FileFormat.Docx2013);

         return memoryStream.ToArray();
      }

    


   public ActionResult Index()
        {
            return View();
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

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

      public JsonResult GetBase64(int id)
      {
         var byteArray = CreateWordDocument2();
         var Base64 = Convert.ToBase64String(byteArray);
         return Json("data:application/ms-word;base64,0M8R4KGxGuEAAAAAAAAAAAAAAAAAAAAAPgADAP7/CQAGAAAAAAAAAAAAAAABAAAAAQAAAAAAAAAAEAAAAgAAAAEAAAD+////AAAAAAAAAAD////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////9////DQAAAP7///8EAAAABQAAAAYAAAAHAAAACAAAAAkAAAAKAAAACwAAAAwAAAAOAAAA/v////7/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////…",JsonRequestBehavior.AllowGet);
      }
   }
}