using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.JavaScript.Extensions;

namespace ScriptCoreLib.JavaScript.DOM.XML
{
	public static class XDocumentExtensions
	{
		public static void DownloadToXDocument(this string AssetPath, Action<XDocument> done)
		{


			new IXMLHttpRequest(
				ScriptCoreLib.Shared.HTTPMethodEnum.GET,
				AssetPath,
				rr =>
				{
					var Document = rr.responseXML.ToXDocument();


					done(Document);
				}
			);
		}
	}
}
