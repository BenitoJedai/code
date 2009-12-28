using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace jsc.meta.Library.Web.PHP
{
	public class WebServiceServlet
	{
		public const string WebServiceExtension = ".asmx";

		public string Operation
		{
			get
			{
				var op = (string)ScriptCoreLib.PHP.Native.SuperGlobals.Get["op"];

				return op;
			}
		}

		public string ServiceName
		{
			get
			{
				var c = CommandLine;

				{
					var i = c.IndexOf("?");
					if (i > -1)
						c = c.Substring(0, i);
				}

				{
					var i = c.IndexOf("/");
					if (i > -1)
						c = c.Substring(0, i);
				}

				{
					var i = c.IndexOf(WebServiceExtension);
					if (i > -1)
						return c.Substring(0, i);
				}

				return null;
			}
		}

		public string WebMethod
		{
			get
			{
				var c = CommandLine;
				var j = -1;

				{
					var i = c.IndexOf("/");
					if (i > -1)
						c = c.Substring(0, i);

					j = i;
				}

				{
					var i = c.IndexOf(WebServiceExtension);
					if (i > -1)
						if (j > -1)
							return CommandLine.Substring(j + 1);
				}

				return null;
			}
		}

		// /Service1.asmx/HelloWorld
		public string CommandLine
		{
			get
			{
				// /WebService1/Service1.asmx
				var REQUEST_URI = (string)ScriptCoreLib.PHP.Native.SuperGlobals.Server[ScriptCoreLib.PHP.Native.SuperGlobals.ServerVariables.REQUEST_URI];
				// /WebService1/index.php
				var SCRIPT_NAME = (string)ScriptCoreLib.PHP.Native.SuperGlobals.Server[ScriptCoreLib.PHP.Native.SuperGlobals.ServerVariables.SCRIPT_NAME];

				var i = SCRIPT_NAME.LastIndexOf("/");
				if (i < 0)
					i = 0;

				var p = REQUEST_URI.Substring(i);

				p = p.Substring(1);

				return p;
			}
		}

		public void Invoke()
		{
			// no dice?

			Console.WriteLine("hello world :) one step closer to having ASP.NET web services in php!");
			Console.WriteLine(@"<hr \>");


			// http://localhost:58527/Service1.asmx
			// http://localhost:58527/Service1.asmx?op=HelloWorld
			// http://localhost:58527/Service1.asmx/HelloWorld

			Console.WriteLine("CommandLine: " + CommandLine + "<br />");
			Console.WriteLine("Operation: " + Operation + "<br />");
			Console.WriteLine("ServiceName: " + ServiceName + "<br />");
			Console.WriteLine("WebMethod: " + WebMethod + "<br />");

		}

		//public abstract void InvokeWebService(InvokeWebServiceArguments a);
	}
}
