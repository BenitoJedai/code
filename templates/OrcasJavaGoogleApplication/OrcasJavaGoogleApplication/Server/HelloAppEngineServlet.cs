using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using javax.servlet.http;
using java.net;
using java.io;

using OrcasJavaGoogleApplication.Server.Library;

namespace OrcasJavaGoogleApplication.Server
{
	[Script]
	public class HelloAppEngineServlet : HttpServlet
	{
		protected override void doGet(HttpServletRequest req, HttpServletResponse resp)
		{
			try
			{
				resp.setContentType("text/html");

				var Path = req.getServletPath();
				var Query = req.getQueryString();
				var PathAndQuery = Path + "?" + Query;

				resp.getWriter().println(Launch(PathAndQuery));
			}
			catch
			{
				// either swallow of throw a runtime exception
			}
		}


		private static string Launch(string PathAndQuery)
		{
			var w = new StringBuilder();

			w.AppendLine("<p>This application was written in C# and was crosscompiled to java by <a href='http://jsc.sf.net'>jsc</a>.</p>");
			w.AppendLine("<p>Visit <a href='http://zproxy.wordpress.com'>author's blog</a>.</p>");
			w.AppendLine("<p>Look at the <a href='http://jsc.svn.sourceforge.net/viewvc/jsc/templates/OrcasJavaGoogleApplication/OrcasJavaGoogleApplication/'>source code</a>.</p>");


			w.AppendLine("<pre>PathAndQuery: " + PathAndQuery + "</pre>");

			var x = new Uri("http://example.com/").ToWebString();

			w.AppendLine(x);

			return w.ToString();
		}
	}
}
