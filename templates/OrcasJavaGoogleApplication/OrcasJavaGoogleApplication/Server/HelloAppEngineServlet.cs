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

				var w = new StringBuilder();

				doGet(w);


				resp.getWriter().println(w.ToString());
			}
			catch
			{
				// either swallow of throw a runtime exception
			}
		}


		private static void doGet(StringBuilder w)
		{
			w.AppendLine("<p>This application was written in C# and was crosscompiled to java by <a href='http://jsc.sf.net'>jsc</a>.</p>");
			w.AppendLine("<p>Visit <a href='http://zproxy.wordpress.com'>author's blog</a>.</p>");
			w.AppendLine("<p>Look at the <a href='http://jsc.svn.sourceforge.net/viewvc/jsc/templates/OrcasJavaGoogleApplication/OrcasJavaGoogleApplication/'>source code</a>.</p>");


			var x = new Uri("http://example.com/").ToWebString();

			w.AppendLine(x);

		}
	}
}
