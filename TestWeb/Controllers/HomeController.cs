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


namespace TestWeb.Controllers
{
    public class HomeController : Controller
    {

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
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}