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

			#region ServiceName
			var REQUEST_URI = (string)ScriptCoreLib.PHP.Native.SuperGlobals.Server[ScriptCoreLib.PHP.Native.SuperGlobals.ServerVariables.REQUEST_URI];
			var SCRIPT_NAME = (string)ScriptCoreLib.PHP.Native.SuperGlobals.Server[ScriptCoreLib.PHP.Native.SuperGlobals.ServerVariables.SCRIPT_NAME];

			var i = SCRIPT_NAME.LastIndexOf("/");
			if (i < 0)
				i = 0;

			var p = REQUEST_URI.Substring(i);

			var a = new InvokeWebServiceArguments();

			// http://zproxy.planet.ee/e/WebService1.aspx?op=e
			// /WebService1.aspx?op=e

			var ServiceName = p.Substring(1);

			i = ServiceName.IndexOf("/");
			if (i >= 0)
				ServiceName = ServiceName.Substring(0, i);
			#endregion


			//Console.WriteLine(REQUEST_URI + @"<br \>");
			//Console.WriteLine(SCRIPT_NAME + @"<br \>");
			//Console.WriteLine(p + @"<br \>");

			a.ServiceName = ServiceName;

			a.RenderMethodsToDocumentContent();

			if (a.DocumentContent != null)
				Console.Write(a.DocumentContent);

			ScriptCoreLib.PHP.Native.API.phpinfo();
		}

		//public abstract void InvokeWebService(InvokeWebServiceArguments a);
	}
}
