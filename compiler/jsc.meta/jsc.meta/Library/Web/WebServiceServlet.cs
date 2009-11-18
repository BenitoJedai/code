using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jsc.meta.Library.Web
{
	public abstract class WebServiceServlet : javax.servlet.http.HttpServlet
	{
		// this class is a template
		// this class cannot be used in .net

		protected override void doGet(javax.servlet.http.HttpServletRequest req, javax.servlet.http.HttpServletResponse resp)
		{
			try
			{

				resp.setContentType("text/xml; charset=utf-8");

				var a = new InvokeWebServiceArguments
				{
					req = req
				};

				InvokeWebService(a);

				resp.getWriter().println(a.Content);
				resp.getWriter().flush();
			}
			catch
			{

			}
		}

		public class InvokeWebServiceArguments
		{
			public javax.servlet.http.HttpServletRequest req;

			public string GetMethodName()
			{
				return "";
			}

			public string GetString(string name)
			{
				return "";
			}

			public void SetReturnParameterString(string value)
			{
				// we really should escape value for xml...

				this.Content = "<?xml version='1.0' encoding='utf-8'?><string xmlns='http://tempuri.org/''>" + value + "</string>";
			}

			public string Content = "";
		}

		public abstract void InvokeWebService(InvokeWebServiceArguments a);
	}
}
