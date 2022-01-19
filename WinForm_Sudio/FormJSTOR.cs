using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm_Sudio
{
	public partial class FormJSTOR : Form
	{
		public FormJSTOR()
		{
			InitializeComponent();

			txtKeyword.Text = "Korean Music in Historical Perspective";
			//txtKeyword.Text = "Korean Music in Historical Perspective";

			txtKeyword.Text = "Korea and the Koreans: In the Mirror of Their Language and History";

			//string s = buildQuery("Australia’s");

			//var d = URLEncode("Australia’s");

			//MessageBox.Show(s);
		}
		private string service_url = "https://www.jstor.org/action/doXmlSearch?version=1.1&operation=searchRetrieve&recordSchema=info%3Asrw/schema/1/dc-v1.1&recordPacking=xml";

		

		private void button1_Click(object sender, EventArgs e)
		{
			string searchKey = buildQuery(txtKeyword.Text);


			string timestamp = URLEncode(DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.ffffff"));

			string otherFilter = string.Empty;

			string APIKey = "c9c01502-14b4-44f1-abd8-9ffc4e9e726f";
			string APISecret = "16ed8480-2bea-4fa9-bd03-4306b0d9d15b";

			//string APIKey = "a8b29f83-b505-4dd3-9f14-a42723d775d2";
			//string APISecret = "3978eb88-eeaa-4539-b34f-22888dd43eef";

			string url = string.Format("{0}&maximumRecords=10&query={2}{3}&startRecord={1}&timestamp={4}&key={5}&signature={6}"
							, service_url, 1, searchKey, otherFilter, timestamp, APIKey
							, BuildSign(10, 1, timestamp, txtKeyword.Text, otherFilter, APIKey, APISecret));

			txtSearchURL.Text = url;

		}

		private string buildQuery(string strKeyword)
		{


			string strResult = string.Empty;

			foreach (string str in strKeyword.Split(' '))
			{
				if (string.IsNullOrEmpty(str) == false)
				{
					if (strResult.Length>0)
					{
						strResult += "+";

					}


					var newStr = URLEncode(EscapeUriString(str));

					if (newStr.Contains("%25E2%2580%2599") && str.Contains("’"))
					{
						newStr = newStr.Replace("%25E2%2580%2599", "’");
					}

					strResult += newStr;


				}
			}
			strResult = "dc.title+%3D+%22" + strResult + "%22";

			return strResult;

		}

		private string buildQuery2(string strKeyword)
		{


			string strResult = string.Empty;

			foreach (string str in strKeyword.Split(' '))
			{
				if (string.IsNullOrEmpty(str) == false)
				{
					if (strResult.Length > 0)
					{
						strResult += "%20";

					}
					var newStr =  URLEncode(EscapeUriString(str));

					if(newStr.Contains("%25E2%2580%2599") && str.Contains("’"))
					{
						newStr = newStr.Replace("%25E2%2580%2599", "’");
					}


					strResult += newStr;
				}
			}
			strResult = "dc.title%20%3D%20%22" + strResult + "%22";

			return strResult;

		}

		private string URLEncode(string str)
		{
			return WebUtility.UrlEncode(str);
		}

		private string EscapeUriString(string str)
		{



			string lower = Uri.EscapeUriString(str);
			Regex reg = new Regex(@"%[a-f0-9]{2}");
			string upper = reg.Replace(lower, m => m.Value.ToUpperInvariant());

			return upper;
		}

		private string BuildSign(int maximumRecords, int startRecord, string timestamp, string query, string otherFilter, string apiKey, string apiSecret)
		{


			string http_method = "GET";
			string host = "www.jstor.org";
			string path = "/action/doXmlSearch";
			string queryForSign = string.Format(@"version=1.1&operation=searchRetrieve&recordSchema=info%3Asrw/schema/1/dc-v1.1&recordPacking=xml&maximumRecords={2}&query={0}&startRecord={1}&timestamp={3}&key={4}"
						, buildQuery2(query + otherFilter),
						startRecord, maximumRecords
						, timestamp
						, apiKey);


			string str_to_sign = string.Format("{0}\n{1}\n{2}\n{3}", http_method, host, path, queryForSign);



			return SHA256(apiSecret, str_to_sign);
		}

		public string SHA256(string Secret, string Message)
		{
			var encoding = new System.Text.UTF8Encoding();
			byte[] keyByte = encoding.GetBytes(Secret);
			byte[] messageBytes = encoding.GetBytes(Message);

			using (HMACSHA256 hmac = new HMACSHA256(keyByte))
			{
				byte[] hmBytes = hmac.ComputeHash(messageBytes);

				return ToHexString(hmBytes);
			}
		}

		public string ToHexString(byte[] array)
		{
			StringBuilder hex = new StringBuilder(array.Length * 2);
			foreach (byte b in array)
			{
				hex.AppendFormat("{0:X2}", b);
			}
			return hex.ToString();
		}
	}
}
