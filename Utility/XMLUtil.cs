using System.IO;
using System.Text;
using System.Xml;

namespace Utility
{
	/// <summary>
	/// Create By Niko 2021-10-20 Ver 8.8.2
	/// </summary>
	public class XMLUtil
	{
		public static XmlDocument ToXML(string strResult)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(strResult);

			return xmlDocument;
		}

		public static XmlDocument ToXML(Stream stream)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = null;
			using (var reader = new StreamReader(stream, Encoding.Default))
			{
				xmlDocument.Load(reader);
			}

			return xmlDocument;
		}

		public static XmlDocument ToXML(Stream stream, Encoding encoding)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = null;
			using (var reader = new StreamReader(stream, encoding))
			{
				xmlDocument.Load(reader);
			}

			return xmlDocument;
		}
	}
}
