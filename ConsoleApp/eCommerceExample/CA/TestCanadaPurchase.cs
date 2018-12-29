﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Moneris;

namespace eCommerceExample.CA
{
    public class CanadaPurchaseTest
    {
		public static void Test(string StoreName,string Api_Token,string CreditCardNumber)
        {
            string order_id = "Test" + DateTime.Now.ToString("yyyyMMddhhmmss");
            //string store_id = "store5";
            string store_id = StoreName;
            //string api_token = "yesguy";
            string api_token = Api_Token;
            string amount = "5.00";
            string pan = "4242424242424242";
            string expdate = "1901"; //YYMM format
            string crypt = "7";
            string processing_country_code = "CA";
            bool status_check = false;

			CofInfo cof = new CofInfo();
			cof.SetPaymentIndicator("U");
			cof.SetPaymentInformation("2");
			cof.SetIssuerId("168451306048014");

            Purchase purchase = new Purchase();
            purchase.SetOrderId(order_id);
            purchase.SetAmount(amount);
            ///Credit card number 
            purchase.SetPan(pan);
            purchase.SetExpDate("2011");
            purchase.SetCryptType(crypt);
            purchase.SetDynamicDescriptor("2134565");
            //purchase.SetWalletIndicator(""); //Refer to documentation for details
            //purchase.SetCofInfo(cof);

            //Optional - Set for Multi-Currency only
            //setAmount must be 0.00 when using multi-currency
            //purchase.SetMCPAmount("500"); //penny value amount 1.25 = 125
            //purchase.SetMCPCurrencyCode("840"); //ISO-4217 country currency number
            //purchase.SetCmId("8nAK8712sGaAkls56"); //set only for usage with Offlinx - Unique max 50 alphanumeric characters transaction id generated by merchant



            HttpsPostRequest mpgReq = new HttpsPostRequest();

            var sp = ServicePointManager.SecurityProtocol;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            mpgReq.SetProcCountryCode(processing_country_code);
            mpgReq.SetTestMode(true); //false or comment out this line for production transactions
            mpgReq.SetStoreId(store_id);
            mpgReq.SetApiToken(api_token);
            mpgReq.SetTransaction(purchase);
            mpgReq.SetStatusCheck(status_check);
            mpgReq.Send();

            try
            {
                Receipt receipt = mpgReq.GetReceipt();
                ServicePointManager.SecurityProtocol = sp;

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
                Console.WriteLine("IsVisaDebit = " + receipt.GetIsVisaDebit());
                Console.WriteLine("HostId = " + receipt.GetHostId());
                Console.WriteLine("MCPAmount = " + receipt.GetMCPAmount());
                Console.WriteLine("MCPCurrencyCode = " + receipt.GetMCPCurrencyCode());
                Console.WriteLine("IssuerId = " + receipt.GetIssuerId());
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
