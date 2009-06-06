using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using javax.servlet.http;

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

				w.Append("<p>This application was written in C# and was crosscompiled to java by <a href='http://jsc.sf.net'>jsc</a>.</p>");
				w.Append("<p>Visit <a href='http://zproxy.wordpress.com'>author's blog</a>.</p>");

				resp.getWriter().println(w.ToString());
			}
			catch
			{
				// either swallow of throw a runtime exception
			}
		}
	}
}
