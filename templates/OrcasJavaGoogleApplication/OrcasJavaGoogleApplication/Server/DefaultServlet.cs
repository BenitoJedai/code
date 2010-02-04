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
	[ConfigurationProvider.UrlPattern]
	public class DefaultServlet : HttpServlet
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
			// http://jsc-project.appspot.com/

			var w = new StringBuilder();

			//w.AppendLine("<h1><b>jsc solutions</b> and compiler services</h1>");
			//w.AppendLine("<p>Visit <a href='http://zproxy.wordpress.com'>author's blog</a>.</p>");

			w.Append(Pages.jsc_solutions.Static.DocumentHTML);

			return w.ToString();
		}
	}
}
