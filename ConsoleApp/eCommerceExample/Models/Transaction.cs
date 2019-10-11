using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moneris;

namespace eCommerceExample.Models
{
   public class Transaction
   {

      public Merchant Merchant { get; set; }





      /// <summary>
      /// Merchant-defined transaction identifier that must be unique for every Purchase, 
      /// Pre-Authorization and Independent Refund transaction. No two transactions of these types may have the same order ID.
      /// For Refund, Completion and Purchase Correction transactions, the order ID must be the same as that of the original transaction.
      /// The last 10 characters of the order ID are displayed in the “Invoice Number” field on the Merchant Direct Reports.
      /// However only letters, numbers and spaces are sent to Merchant Direct.
      /// A minimum of 3 and a maximum of 10 valid characters are sent to Merchant Direct.
      /// Only the last characters beginning after any invalid characters are sent.
      /// For example, if the order ID is 1234-567890, only 567890 is sent to Merchant Direct.
      /// If the order ID has fewer than 3 characters, it may display a blank or 0000000000 in the Invoice Number field. 
      /// </summary>
      public string OrderId { get; set; }

      /// <summary>
      /// Used when performing follow-on transactions. (That is, Completion, Purchase Correction or Refund.)
      /// This must be the value that was returned as the transaction number in the response of the original transaction.
      /// When performing a Completion, this value must reference the Pre-Authorization.
      /// When performing a Refund or a Purchase Correction, this value must reference the Completion or the Purchase.
      /// </summary>
      public string TxnNumber { get; set; }
      /// <summary>
      /// Most credit card numbers today are 16 digits, but some 13-digit numbers are still accepted by some issuers. 
      /// This field has been intentionally expanded to 20 digits in consideration for future expansion and potential support of private label card ranges. 
      /// </summary>
      public string PAN { get; set; }
      /// <summary>
      /// Submit in YYMM format.
      /// Note: This is the reverse of the date displayed on the physical card, which is MMYY.
      /// </summary>
      public string ExpDate { get; set; }


      /// <summary>
      /// This can be used for policy number, membership number, student ID, invoice number.
      /// This field is searchable from the Moneris Merchant Resource Centre.
      /// </summary>
      public string CustId { get; set; }
      /// <summary>
      /// Transaction amount. Used in a number of transactions. 
      /// Note that this is different from the amount used in a Completion transaction, which is an alphanumeric value.
      /// This must contain at least 3 digits, two of which are penny values.
      /// The minimum allowable value is $0.01, and the maximum allowable value is 9999999.99. 
      /// Transaction amounts of $0.00 are not allowed.
      /// </summary>
      public string Amount { get; set; }

      public string Track2 { get; set; }

      /// <summary>
      /// Status Check is a connection object value that allows merchants to verify whether a previously sent transaction was processed successfully.
      /// To submit a Status Check request, resend the original transaction with all the same parameter values, 
      /// but set the status check value to either true or false. 
      /// Once set to “true”, the gateway will check the status of a transaction that has an order_id that matches the one passed.
      ///   If the transaction is found, the gateway will respond with the specifics of that transaction.
      ///   If the transaction is not found, the gateway will respond with a not found message.
      ///   Once it is set to “false”, the transaction will process as a new transaction.
      /// 
      /// Things to consider:
      ///   The Status Check request should only be used once and immediately(within 2 minutes) after the last transaction that had failed.
      ///   Do not resend the Status Check request if it has timed out. Additional investigation is required.
      /// </summary>
      public bool StatusCheck { get; set; }

      /// <summary>
      /// Merchant defined description sent on a per-transaction basis that will appear on the credit card statement.  
      /// Dependent on the card Issuer, the statement will typically show the dynamic descriptor appended to the merchant's existing business name separated by the "/" character.  Please note that the combined length of the merchant's business name, forward slash "/" character, and the dynamic descriptor may not exceed 22 characters.
      /// 
      /// Example-
      /// Existing Business Name:  ABC Painting
      /// Dynamic Descriptor:  Booking 12345
      /// Cardholder Statement Displays:  ABC Painting/Booking 1
      /// </summary>
      public string DynamicDescriptor { get; set; }

      public TransactionType CurrentTransactionType { get; set; }


      public CustInfo CustomerInfo { get; set; }

      /// <summary>
      /// Supported values are:
      ///1 - Mail Order / Telephone Order—Single
      ///2 - Mail Order / Telephone Order—Recurring
      ///3 - Mail Order / Telephone Order—Instalment
      ///4 - Mail Order / Telephone Order—Unknown classification
      ///5 - Authenticated e-commerce transaction(VBV)
      ///6 - Non-authenticated e-commerce transaction(VBV)
      ///7 - SSL-enabled merchant
      ///8 - Non-secure transaction(web- or email-based)
      ///9 - SET non-authenticated transaction
      /// </summary>
      public string CrtpyType { get; set; }

      /// <summary>
      /// Merchant defined description sent on a per-transaction basis that will appear on the credit card statement. 
      /// Dependent on the card Issuer, the statement will typically show 
      /// the dynamic descriptor appended to the merchant's existing business name separated by the "/" character.  
      /// Please note that the combined length of the merchant's business name, 
      /// forward slash "/" character, and the dynamic descriptor may not exceed 22 characters.
      /// </summary>
      public string DynamicDes;



      public enum TransactionType
      {
         Purchase = 100,
         InteracOnlinePurchase = 101,
         Refund = 200,
         InteracOnlineRefund = 201,
         CardVerification = 300,

      }

   }



}
