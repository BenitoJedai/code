using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jsc.meta.Library.Web
{
	public abstract partial class WebServiceServlet : javax.servlet.http.HttpServlet
	{
		// this class is a template
		// this class cannot be used in .net

		protected override void doGet(javax.servlet.http.HttpServletRequest req, javax.servlet.http.HttpServletResponse resp)
		{
			try
			{


				var a = new InvokeWebServiceArguments
				{
					req = req
				};

				InvokeWebService(a);

				//Console.WriteLine("req: " + req.getPathInfo());

				if (a.Content != null)
				{
					resp.setContentType("text/xml; charset=utf-8");
					resp.getWriter().println(a.Content);
				}
				else
				{
					resp.setContentType("text/html");

					var Content = a.DocumentContent;

					if (Content == null)
						Content = "Sorry! The named method was not found!";

					resp.getWriter().println(Content);
				}

				resp.getWriter().flush();
			}
			catch (csharp.ThrowableException exc)
			{
				//Console.WriteLine("error!");
				((java.lang.Throwable)(object)exc).printStackTrace();
			}
		}

		public abstract void InvokeWebService(InvokeWebServiceArguments a);
	}
}
