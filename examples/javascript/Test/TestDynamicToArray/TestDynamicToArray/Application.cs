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
using TestDynamicToArray;
using TestDynamicToArray.Design;
using TestDynamicToArray.HTML.Pages;

namespace TestDynamicToArray
{
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
			// X:\jsc.svn\examples\javascript\async\AsyncHopToUIFromWorker\AsyncHopToUIFromWorker\Application.cs
			// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201504/20150401

			var a = new { a = new[] { 1, 2, 3 } };


			dynamic x = a;

			// Uncaught ReferenceError: type$AAAAAAAAAAAAAAAAAAAAAA is not defined
			int[] z = x.a;

			new IHTMLPre { new { z.Length } }.AttachToDocument();


		}

	}
}
