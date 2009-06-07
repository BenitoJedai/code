using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using java.io;
using java.net;
using javax.servlet.http;
using OrcasJavaGoogleApplication.Server.Library;
using ScriptCoreLib;
using ScriptCoreLibJava.Extensions;

namespace OrcasJavaGoogleApplication.Server
{
	[Script]
	[ConfigurationProvider.UrlPattern("/OrcasJavaGoogleApplication/*")]
	public class HelloAppEngineServlet : HttpServlet
	{
		protected override void doGet(HttpServletRequest req, HttpServletResponse resp)
		{
			try
			{
				resp.setContentType("text/html; charset=utf-8");
				resp.getWriter().println(Launch(req.GetPathAndQuery()));
			}
			catch
			{
				// either swallow of throw a runtime exception
			}
		}


		private static string Launch(string PathAndQuery)
		{
			// published at:
			// http://jsc-project.appspot.com/OrcasJavaGoogleApplication/

			var w = new StringBuilder();

			w.AppendLine("<h1>指事字</h1>");
			w.AppendLine("<p>This application was written in C# and was crosscompiled to java by <a href='http://jsc.sf.net'>jsc</a>.</p>");
			w.AppendLine("<p>Visit <a href='http://zproxy.wordpress.com'>author's blog</a>.</p>");
			w.AppendLine("<p>Look at the <a href='http://jsc.svn.sourceforge.net/viewvc/jsc/templates/OrcasJavaGoogleApplication/OrcasJavaGoogleApplication/'>source code</a>.</p>");


			w.AppendLine("<pre>PathAndQuery: " + PathAndQuery + "</pre>");

			//var x = new Uri("http://example.com/").ToWebString();
			var x = new Uri("http://www.google.co.jp/").ToWebString();

			w.AppendLine(x);

			return w.ToString();
		}
	}
}
