using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using eCommerceExample;
using eCommerceExample.CA;
using eCommerceExample.Models;

namespace TestWeb.Controllers
{
   public class MonerisController : Controller
   {
      // GET: Moneris
      public ActionResult Index()
      {
         return View();
      }


      public ActionResult Purchase()
      {
         ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
         ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;


         ViewBag.r1 = TestCanadaIDebitPurchase.WebTest(true);

         ViewBag.r2 = TestCanadaIDebitPurchase.WebTest(false);

         return View();
      }

      public ActionResult Refund()
      {
         string Result = TestCanadaIDebitRefund.WebTest();

         ViewBag.Result = Result;

         return View();
      }


      public ActionResult Test(int id)
      {
         Merchant merchant1;

         if (id == 1)
         {
            merchant1 = new Merchant("store5", "yesguy", Merchant.EnvironmentType.QA, Merchant.ProcessingCountryType.CA);

         }
         else
         {
            merchant1 = new Merchant("monca03090", "LMXhZSCbryck8EOjTS93", Merchant.EnvironmentType.QA, Merchant.ProcessingCountryType.CA);
         }


         Transaction transaction = new Transaction
         {
            Merchant = merchant1,
            OrderId = "Test" + DateTime.Now.ToString("yyyyMMddhhmmss"),
            CustId = "Lance_Briggs_55",
            Amount = "5.00",
            Track2 = "5268051119993326=0609AAAAAAAAAAAAA000",
            CurrentTransactionType = Transaction.TransactionType.InteracOnlinePurchase,
         };

         MonerisTransacton monerisTransacton = new MonerisTransacton();

         var response =  monerisTransacton.PerformTransaction(transaction);

         return View(response);

      }


      private bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
      {
         return true;
      }
   }
}