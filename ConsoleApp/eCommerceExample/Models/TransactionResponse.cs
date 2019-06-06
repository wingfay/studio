using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceExample.Models
{
   public class TransactionResponse
   {

      public string CardType { get; set; }

      public string TransAmount { get; set; }

      public string TxnNumber { get; set; }
      public string ReceiptId { get; set; }
      public string TransType { get; set; }
      public string ReferenceNum { get; set; }
      public string ResponseCode { get; set; }
      public string ISO { get; set; }
      public string BankTotals { get; set; }
      public string Message { get; set; }
      public string AuthCode { get; set; }
      public string Complete { get; set; }
      public string TransDate { get; set; }

      public string TransTime { get; set; }
      public string Ticket { get; set; }

      public string TimedOut { get; set; }


      public Exception Exception { get; set; }


   }
}
