using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;
using System;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
	[Script(InternalConstructor = true)]
	public class IHTMLApplet : IHTMLElement
	{
		public string code;
		public string codebase;
		public string archive;
		public bool mayscript;


		#region Constructor

		public IHTMLApplet()
		{
			// InternalConstructor
		}

		static IHTMLApplet InternalConstructor()
		{
			return (IHTMLApplet)InternalConstructor(HTMLElementEnum.applet);
		}

		#endregion

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
			internal static void CombineDelegate(IHTMLApplet a, Action value)
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

			private static void Tick(IHTMLApplet a, Action value, Timer t)
			{
				// http://www.rgagnon.com/javadetails/java-0176.html

				// in IE: isActive returns an error if the applet IS loaded, 
				// false if not loaded
				// in NS: isActive returns true if loaded, an error if not loaded, 

				var ie = (bool)new IFunction(
					"/*@cc_on return true; @*/ return false;"
				).apply(null);

				var r = false;

				try
				{
					r = a.isActive();
				}
				catch
				{
					r = ie;
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
	}
}
