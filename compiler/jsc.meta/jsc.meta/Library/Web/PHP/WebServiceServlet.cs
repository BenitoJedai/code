using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace jsc.meta.Library.Web.PHP
{
	public class WebServiceServlet
	{
		public void Invoke()
		{
			//Console.WriteLine("hello world :) one step closer to having ASP.NET web services in php!");
			//Console.WriteLine(@"<hr \>");

			var REQUEST_URI = (string)ScriptCoreLib.PHP.Native.SuperGlobals.Server[ScriptCoreLib.PHP.Native.SuperGlobals.ServerVariables.REQUEST_URI];
			var SCRIPT_NAME = (string)ScriptCoreLib.PHP.Native.SuperGlobals.Server[ScriptCoreLib.PHP.Native.SuperGlobals.ServerVariables.SCRIPT_NAME];

			//Console.WriteLine(REQUEST_URI + @"<br \>");
			//Console.WriteLine(SCRIPT_NAME + @"<br \>");

			var i = SCRIPT_NAME.LastIndexOf("/");
			if (i < 0)
				i = 0;

			var p = REQUEST_URI.Substring(i);

			//Console.WriteLine(p + @"<br \>");

			var a = new InvokeWebServiceArguments();

			a.RenderMethodsToDocumentContent();

			Console.Write(a.DocumentContent);
		}

		//public abstract void InvokeWebService(InvokeWebServiceArguments a);
	}
}
