using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moneris;

namespace eCommerceExample.CA
{
   public class TestCanadaIDebitRefund
   {
      public static void Test()
      {
         string store_id = "store5";
         string api_token = "yesguy";
         string order_id = "Test20150625014816";
         string amount = "5.00";
         string txn_number = "113524-0_10";
         string processing_country_code = "CA";
         bool status_check = false;
         IDebitRefund refund = new IDebitRefund();
         refund.SetOrderId(order_id);
         refund.SetAmount(amount);
         refund.SetTxnNumber(txn_number);
         HttpsPostRequest mpgReq = new HttpsPostRequest();
         mpgReq.SetProcCountryCode(processing_country_code);
         mpgReq.SetTestMode(true); //false or comment out this line for production transactions
         mpgReq.SetStoreId(store_id);
         mpgReq.SetApiToken(api_token);
         mpgReq.SetTransaction(refund);
         mpgReq.SetStatusCheck(status_check);
         mpgReq.Send();
         try
         {
            Receipt receipt = mpgReq.GetReceipt();
            Console.WriteLine("CardType = " + receipt.GetCardType());
            Console.WriteLine("TransAmount = " + receipt.GetTransAmount());
            Console.WriteLine("TxnNumber = " + receipt.GetTxnNumber());
            Console.WriteLine("ReceiptId = " + receipt.GetReceiptId());
            Console.WriteLine("TransType = " + receipt.GetTransType());
            Console.WriteLine("ReferenceNum = " + receipt.GetReferenceNum());
            Console.WriteLine("ResponseCode = " + receipt.GetResponseCode());
            Console.WriteLine("ISO = " + receipt.GetISO());
            Console.WriteLine("BankTotals = " + receipt.GetBankTotals());
            Console.WriteLine("Message = " + receipt.GetMessage());
            Console.WriteLine("AuthCode = " + receipt.GetAuthCode());
            Console.WriteLine("Complete = " + receipt.GetComplete());
            Console.WriteLine("TransDate = " + receipt.GetTransDate());
            Console.WriteLine("TransTime = " + receipt.GetTransTime());
            Console.WriteLine("Ticket = " + receipt.GetTicket());
            Console.WriteLine("TimedOut = " + receipt.GetTimedOut());
            Console.ReadLine();
         }
         catch (Exception e)
         {
            Console.WriteLine(e);
         }
      }

      public static string WebTest()
      {
         string store_id = "store5";
         string api_token = "yesguy";
         string order_id = "Test20150625014816";
         string amount = "5.00";
         string txn_number = "113524-0_10";
         string processing_country_code = "CA";
         bool status_check = false;
         IDebitRefund refund = new IDebitRefund();
         refund.SetOrderId(order_id);
         refund.SetAmount(amount);
         refund.SetTxnNumber(txn_number);
         HttpsPostRequest mpgReq = new HttpsPostRequest();
         mpgReq.SetProcCountryCode(processing_country_code);
         mpgReq.SetTestMode(true); //false or comment out this line for production transactions
         mpgReq.SetStoreId(store_id);
         mpgReq.SetApiToken(api_token);
         mpgReq.SetTransaction(refund);
         mpgReq.SetStatusCheck(status_check);
         mpgReq.Send();

         StringBuilder stringBuilder = new StringBuilder();

         try
         {
            Receipt receipt = mpgReq.GetReceipt();
            stringBuilder.AppendLine("CardType = " + receipt.GetCardType() + "<br/>");
            stringBuilder.AppendLine("TransAmount = " + receipt.GetTransAmount() + "<br/>");
            stringBuilder.AppendLine("TxnNumber = " + receipt.GetTxnNumber() + "<br/>");
            stringBuilder.AppendLine("ReceiptId = " + receipt.GetReceiptId() + "<br/>");
            stringBuilder.AppendLine("TransType = " + receipt.GetTransType() + "<br/>");
            stringBuilder.AppendLine("ReferenceNum = " + receipt.GetReferenceNum() + "<br/>");
            stringBuilder.AppendLine("ResponseCode = " + receipt.GetResponseCode() + "<br/>");
            stringBuilder.AppendLine("ISO = " + receipt.GetISO() + "<br/>");
            stringBuilder.AppendLine("BankTotals = " + receipt.GetBankTotals() + "<br/>");
            stringBuilder.AppendLine("Message = " + receipt.GetMessage() + "<br/>");
            stringBuilder.AppendLine("AuthCode = " + receipt.GetAuthCode() + "<br/>");
            stringBuilder.AppendLine("Complete = " + receipt.GetComplete() + "<br/>");
            stringBuilder.AppendLine("TransDate = " + receipt.GetTransDate() + "<br/>");
            stringBuilder.AppendLine("TransTime = " + receipt.GetTransTime() + "<br/>");
            stringBuilder.AppendLine("Ticket = " + receipt.GetTicket() + "<br/>");
            stringBuilder.AppendLine("TimedOut = " + receipt.GetTimedOut() + "<br/>");
         }
         catch (Exception e)
         {
            stringBuilder.AppendLine(e.Message);
            stringBuilder.AppendLine(e.StackTrace);
         }

         return stringBuilder.ToString();
      }
   }

}
