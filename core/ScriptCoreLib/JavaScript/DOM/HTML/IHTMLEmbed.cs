using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared;
using System;
using ScriptCoreLib.JavaScript.DOM.XML;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
	[Script(InternalConstructor = true)]
	public class IHTMLEmbedFlash : IHTMLEmbed
	{
		public string CallFunction(string e)
		{
			// IHTMLEmbed = flash10 ? :)

			return null;
		}
	}

	[Script]
	public static class IHTMLEmbedFlashExtensions
	{
		public static string CallFunction(this IHTMLEmbedFlash e, string method, string[] args)
		{
			var xml = new ScriptCoreLib.JavaScript.DOM.XML.IXMLDocument("invoke");

			xml.documentElement.setAttribute("name", method);
			xml.documentElement.setAttribute("returntype", "xml");

			var _arguments = xml.createElement("arguments");

			foreach (var item in args)
			{
				if (item == null)
				{
					_arguments.appendChild(xml.createElement("null"));
				}
				else
				{
					var _string = xml.createElement("string");

					_string.appendChild(xml.createTextNode(item));

					_arguments.appendChild(_string);
				}

			}
			xml.documentElement.appendChild(_arguments);

			var responseText = e.CallFunction(
				xml.ToXMLString()
			);

			var responseValue = IXMLDocument.Parse(responseText).documentElement.text;

			return responseValue;
		}
	}

	[Script(InternalConstructor = true)]
	public class IHTMLEmbed : IHTMLElement
	{


		#region Constructor

		public IHTMLEmbed()
		{
			// InternalConstructor
		}


		static IHTMLObject InternalConstructor()
		{
			return (IHTMLObject)IHTMLElement.InternalConstructor(HTMLElementEnum.embed);
		}

		#endregion

		public string src;
		public string autostart;
		public string volume;
		public string type;
		public string wmode;



	

	}
}
