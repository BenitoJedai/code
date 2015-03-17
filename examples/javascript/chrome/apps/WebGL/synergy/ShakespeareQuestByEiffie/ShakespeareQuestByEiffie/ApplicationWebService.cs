using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShakespeareQuestByEiffie
{
	/// <summary>
	/// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
	/// </summary>
	public class ApplicationWebService
	{
		/// <summary>
		/// The static content defined in the HTML file will be update to the dynamic content once application is running.
		/// </summary>
		public XElement Header = new XElement(@"h1", @"JSC - The .NET crosscompiler for web platforms. ready.");

		/// <summary>
		/// This Method is a javascript callable method.
		/// </summary>
		/// <param name="e">A parameter from javascript.</param>
		/// <param name="y">A callback to javascript.</param>
		public void WebMethod2(string e, Action<string> y)
		{
			// Send it back to the caller.
			y(e);
		}

	}
}
