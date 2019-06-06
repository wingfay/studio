using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moneris;

namespace eCommerceExample.Models
{
   public class TransactionBase: BaseClass
   {
      internal string EcrNo;

      public enum TransactionType
      {
         Purchase = 0,
         Refund = 1,
         CardVerification = 2,
        
      }

      //public string StoreID { get; set; }
      public CustInfo CustomerInfo { get; set; }
      private string StoreId { get; set; }
      private string ApiToken { get; set; }
      public string OrderId { get; set; }
      public string TxnNumber { get; set; }
      public TransactionType transactionType { get; set; }
      public string CrtpyType { get; set; }
      public string PAN { get; set; }
      public string ExpDate { get; set; }
      public string CVD { get; set; }
      public string CustId { get; set; }
      public string Amount { get; set; }
      public string DynamicDes { get; set; }
      public CofInfo cInfo { get; set; }
      public string DataKey { get; internal set; }
      public string IssuerId { get; set; }
      public Recur RecuringCycle { get; set; }
      public string ShipIndicator { get; set; }
      public AvsInfo AvsCheck { get; set; }
      public CvdInfo CvdCheck { get; set; }
      public string Duration { get; internal set; }
      public string OriginalOrderID { get; internal set; }
      public string CAVV { get; set; }
      public string ENC_Track2 { get; internal set; }
      public string DeviceType { get; internal set; }
      public string AuthCode { get; internal set; }
      public ConvFeeInfo ConvFee { get; internal set; }
      public string DataKeyFormat { get; internal set; }
      public bool TestMode { get; internal set; }
      public string CmID { get; internal set; }
      public string MarketIndicator { get; internal set; }

      public TransactionBase()
      {
         this.StoreId = _storeID;
         this.ApiToken = _aPIToken;
         this.CustomerInfo = new CustInfo();
      }



      public void setProdcution()
      {
         base.SetProdcutionEnvironment();
      }
   }
}
