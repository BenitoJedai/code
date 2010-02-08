using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared;
using System;

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
			xml.documentElement.setAttribute("returntype", "javascript");

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

			return e.CallFunction(
				xml.ToXMLString()
			);
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



		#region event onload hack

		#region event onload
		public event Action onload
		{
			// http://www.rgagnon.com/javadetails/java-0176.html
			// http://bytes.com/topic/javascript/answers/147231-applet-onload-alert-hi
			// http://www.irt.org/script/4013.htm
			// http://www.blooberry.com/indexdot/html/tagpages/attributes/onload.htm

			[Script(DefineAsStatic = true)]
			add
			{
				//Native.Window.alert("add_onload");

				__onload.CombineDelegate(this, value);
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				throw new NotSupportedException();
			}
		}
		#endregion

		/// <summary>
		/// Method for determining whether or not the applet is active. An applet is activated right before its start() method is called and deactivated right after its stop() method is called.
		/// </summary>
		/// <returns>The Boolean value is true if the applet is active, false otherwise.</returns>
		public bool isActive()
		{
			// http://www.informit.com/articles/article.aspx?p=24476

			return false;
		}

		[Script]
		internal static class __onload
		{
			// will HTML5 enable a more nicer solution?

			internal static void CombineDelegate(IHTMLEmbed a, Action value)
			{
				new Timer(
					t =>
					{
						Tick(a, value, t);
					},
					1,
					100
				);
			}

			private static void Tick(IHTMLEmbed a, Action value, Timer t)
			{
				// http://www.rgagnon.com/javadetails/java-0176.html

				// in IE: isActive returns an error if the applet IS loaded, 
				// false if not loaded
				// in NS: isActive returns true if loaded, an error if not loaded, 

				//var ie = (bool)new IFunction(
				//    "/*@cc_on return true; @*/ return false;"
				//).apply(null);

				var r = false;

				try
				{
					r = a.isActive();
				}
				catch
				{
					//r = ie;
				}

				if (r)
				{
					t.Stop();

					if (value != null)
						value();

					// note: this actually works! :)
					//Native.Window.alert("onload!");
				}
			}
		}
		#endregion

	}
}
