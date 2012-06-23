// For more information visit:
// http://studio.jsc-solutions.net

// View as Visual Basic project
// http://do.jsc-solutions.net/View-as-Visual-Basic-project

// View as Visual FSharp project
// http://do.jsc-solutions.net/View-as-Visual-FSharp-project

using System;
using System.Linq;
using System.Xml.Linq;
using ScriptCoreLib;
using ScriptCoreLib.Extensions;

namespace TestSolutionBuilderWithDesigner
{
	/// <summary>
	/// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
	/// </summary>
	public sealed class ApplicationWebService
	{
		/// <summary>
		/// This Method is a javascript callable method.
		/// </summary>
		/// <param name="e">A parameter from javascript</param>
		/// <param name="y">A callback to javascript</param>
		public void WebMethod2(XElement e, Action<XElement> y)
		{
			// Send something back from WebMethod2
			// http://do.jsc-solutions.net/Send-something-back-from-WebMethod2

			e.Element(@"Data").ReplaceAll(@"jsc can convert F# to Java");
			// Send it to the caller.
			y(e);
		}


	}
}
