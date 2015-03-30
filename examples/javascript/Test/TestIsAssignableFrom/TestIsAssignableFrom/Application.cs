using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestIsAssignableFrom;
using TestIsAssignableFrom.Design;
using TestIsAssignableFrom.HTML.Pages;

namespace TestIsAssignableFrom
{
	class foo : IDisposable
	{
		public void Dispose()
		{
		}
	}
	/// <summary>
	/// Your client side code running inside a web browser as JavaScript.
	/// </summary>
	public sealed class Application : ApplicationWebService
	{
		/// <summary>
		/// This is a javascript application.
		/// </summary>
		/// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
		public Application(IApp page)
		{
			var x = new foo();

			new IHTMLPre { new { x } }.AttachToDocument();

			var xIsIDisposable = x is IDisposable;

			new IHTMLPre { new { xIsIDisposable } }.AttachToDocument();


			var xType = x.GetType();

			new IHTMLPre { new { xType } }.AttachToDocument();

			var xIsAssignableFrom = typeof(IDisposable).IsAssignableFrom(xType);

			new IHTMLPre { new { xIsAssignableFrom } }.AttachToDocument();

		}

	}
}

//{{ x = [object Object] }}
//{{ xIsIDisposable = true }}
//{{ xType = foo }}
//{{ xIsAssignableFrom = false }}