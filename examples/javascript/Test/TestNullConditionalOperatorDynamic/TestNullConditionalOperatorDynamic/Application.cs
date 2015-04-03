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
using TestNullConditionalOperatorDynamic;
using TestNullConditionalOperatorDynamic.Design;
using TestNullConditionalOperatorDynamic.HTML.Pages;

namespace TestNullConditionalOperatorDynamic
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
			// X:\jsc.svn\examples\javascript\Test\TestNullConditionalOperator\TestNullConditionalOperator\Application.cs

			var u = Native.document.location.href;

			var x = Native.document as dynamic;


			//	02000002 TestNullConditionalOperatorDynamic.Application
			//	script: error JSC1000: running a newer compiler? opcode unsupported - [0x0084] brtrue.s + 0 - 1{[0x0082]
			//ldloc.s    +1 -0}

			var z = x?.location?.href;

			new IHTMLPre { new { z } }.AttachToDocument();

		}

	}
}
