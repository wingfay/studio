using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using eCommerceExample.Models;
using Moneris;
using Transaction = eCommerceExample.Models.Transaction;

namespace eCommerceExample
{
   /// <summary>
   /// default: Product Mode
   /// 
   /// </summary>
   public class MonerisTransacton
   {


      public TransactionResponse PerformTransaction(Transaction t)
      {



         HttpsPostRequest mpgReq = new HttpsPostRequest();
         mpgReq.SetProcCountryCode(t.Merchant.ProcessingCountry.ToString());
         mpgReq.SetTestMode(t.Merchant.Environment == Merchant.EnvironmentType.QA); //false or comment out this line for production transactions
         mpgReq.SetStoreId(t.Merchant.StoreId);
         mpgReq.SetApiToken(t.Merchant.ApiToken);
         mpgReq.SetStatusCheck(t.StatusCheck);


         TransactionResponse transactionResponse = new TransactionResponse
         {
            Exception = null
         };

         switch (t.CurrentTransactionType)
         {
            case Transaction.TransactionType.Purchase:
               break;
            case Transaction.TransactionType.InteracOnlinePurchase:
               IDebitPurchase IOP_Txn = new IDebitPurchase();
               IOP_Txn.SetOrderId(t.OrderId);
               IOP_Txn.SetCustId(t.CustId);
               IOP_Txn.SetAmount(t.Amount);
               IOP_Txn.SetIdebitTrack2(t.Track2);
               mpgReq.SetTransaction(IOP_Txn);



               break;
            case Transaction.TransactionType.Refund:
               break;
            case Transaction.TransactionType.InteracOnlineRefund:

               IDebitRefund refund = new IDebitRefund();
               refund.SetOrderId(t.OrderId);
               refund.SetAmount(t.Amount);
               refund.SetTxnNumber(t.TxnNumber);
               mpgReq.SetTransaction(refund);

               break;
            case Transaction.TransactionType.CardVerification:
               break;
            default:
               break;
         }

         ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
         ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

         mpgReq.Send();

         try
         {

           
            Receipt receipt = mpgReq.GetReceipt();

            transactionResponse.CardType = receipt.GetCardType();
            transactionResponse.TransAmount = receipt.GetTransAmount();
            transactionResponse.TxnNumber = receipt.GetTxnNumber();
            transactionResponse.ReceiptId = receipt.GetReceiptId();
            transactionResponse.TransType = receipt.GetTransType();
            transactionResponse.ReferenceNum = receipt.GetReferenceNum();
            transactionResponse.ResponseCode = receipt.GetResponseCode();
            transactionResponse.ISO = receipt.GetISO();

            transactionResponse.BankTotals = receipt.GetBankTotals();
            transactionResponse.Message = receipt.GetMessage();
            transactionResponse.AuthCode = receipt.GetAuthCode();
            transactionResponse.CardType = receipt.GetCardType();
            transactionResponse.Complete = receipt.GetComplete();
            transactionResponse.TransDate = receipt.GetTransDate();
            transactionResponse.TransTime = receipt.GetTransTime();
            transactionResponse.Ticket = receipt.GetTicket();
            transactionResponse.TimedOut = receipt.GetTimedOut();


            
         }
         catch (Exception ex)
         {
            transactionResponse.Exception = ex;
         }


         return transactionResponse;


      }

      private bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
      {
         return true;
      }
   }
}
