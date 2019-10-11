using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using eCommerceExample;
using eCommerceExample.CA;
using eCommerceExample.Models;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
         //CanadaPurchaseTest.Test(Global.StoreName,Global.Token,string.Empty);

         //   TestCanadaIDebitPurchase.Test();

         //int unitp5 = 100 / 5;

         //int unitp4 = 100 / 4;

         //int unitp3 = 100 / 3;

         //int unitp6 = 100 / 6;

         //Console.WriteLine("100 / 5:" + unitp5);
         //Console.WriteLine("100 / 4:" + unitp4);
         //Console.WriteLine("100 / 3:" + unitp3);
         //Console.WriteLine("100 / 6:" + unitp6);
         //for (int i = 0; i < 10; i++)
         //{
         //   Console.WriteLine(string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now));
         //}


         //string s = "999998.12";

         //Console.WriteLine($"{s}:{Convert.ToInt32(s.Substring(0, s.IndexOf(".")))}");

         //string s1 = "999998";

         //Console.WriteLine($"{s1}:{Convert.ToInt32(s1.Substring(0, s1.IndexOf(".")>0?s1.IndexOf("."):s1.Length))}");


         var m = new Merchant("store5", "yesguy", Merchant.EnvironmentType.QA, Merchant.ProcessingCountryType.CA);

         Transaction transaction = new Transaction
         {
            Merchant = m,
            OrderId = "201907101612318560",
            Amount = "11.00",
            TxnNumber = "858400-0_14",
            CrtpyType="7",
            CurrentTransactionType = Transaction.TransactionType.Refund,
         };

         MonerisTransacton monerisTransacton = new MonerisTransacton();
         var response = monerisTransacton.PerformTransaction(transaction);

         JavaScriptSerializer jss = new JavaScriptSerializer();
         string myJson = jss.Serialize(response);

         Console.WriteLine(myJson);

         Console.ReadKey();
      }

      public static string CreateInvoice()
      {
         return string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now);
      }

   }
}
