using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp
{
	/// <summary>
	/// 注释是因为这段代码在特定环境才能测试。
	/// </summary>
	public class SAMLTest
	{
		//public void TestSAMLResponseXML()
		//{
		//	var a = Context.Server.MapPath("1.xml");




		//	XmlDocument xmlDocument = new XmlDocument();

		//	xmlDocument.Load(a);


		//	SAMLResponse samlResponse = new SAMLResponse(xmlDocument.DocumentElement);

		//	//SAMLAssertion samlAssertion = new SAMLAssertion(sAMLResponse.Assertions[0] as XmlElement);
		//	// Extract the asserted identity from the SAML response.


		//	SAMLAssertion samlAssertion = null;

		//	if (samlResponse.Assertions.Count > 0)
		//	{
		//		try
		//		{
		//			samlAssertion = (SAMLAssertion)samlResponse.Assertions[0];
		//		}
		//		catch (InvalidCastException)
		//		{

		//			samlAssertion = new SAMLAssertion(samlResponse.Assertions[0] as XmlElement);
		//		}
		//		catch (Exception)
		//		{
		//			throw;
		//		}
		//	}
		//	else
		//	{
		//		throw new Exception("samlResponse Assertions count is Zero.");
		//	}


		//	string SAMLEmailaddress = string.Empty;
		//	string SAMLUsername = string.Empty;

		//	if (samlAssertion.GetAttributeStatements().Count > 0)
		//	{
		//		foreach (SAMLAttribute item in samlAssertion.GetAttributeStatements()[0].GetUnencryptedAttributes())
		//		{
		//			if (item.Name.ToLower().Trim() == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")
		//			{
		//				SAMLEmailaddress = item.Values[0].Data.ToString();
		//				SAMLEmailaddress = SAMLEmailaddress.Trim();

		//			}
		//			if (item.Name.ToLower().Trim() == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/username" ||
		//				item.Name.ToLower().Trim() == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")
		//			{
		//				SAMLUsername = item.Values[0].Data.ToString();

		//				SAMLUsername = SAMLUsername.Trim();
		//			}
		//		}

		//	}


		//	string userName = string.Empty;

		//	if (string.IsNullOrEmpty(CustomSetting.SAMLSSOAttributeName))
		//	{
		//		if (samlAssertion.Subject.NameID != null)
		//		{
		//			userName = samlAssertion.Subject.NameID.NameIdentifier;
		//		}
		//	}
		//	else
		//	{
		//		userName = samlAssertion.GetAttributeValue(CustomSetting.SAMLSSOAttributeName);
		//	}

		//	Trace.Write("SP", samlResponse.ToXml().OuterXml);
		//}
	}
}
