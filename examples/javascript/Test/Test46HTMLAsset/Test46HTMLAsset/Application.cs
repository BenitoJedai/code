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
using Test46HTMLAsset;
using Test46HTMLAsset.Design;
using Test46HTMLAsset.HTML.Pages;

namespace Test46HTMLAsset
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
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150305/assets
            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeAppWindowMouseCapture\ChromeAppWindowMouseCapture\Application.cs


            new MyShadow { }

			// how did it ever work?
			.AttachToDocument();
		}

	}
}
