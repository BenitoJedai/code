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


				var a = new InvokeWebServiceArguments
				{
					req = req
				};

				InvokeWebService(a);

				if (a.Content != null)
				{
					resp.setContentType("text/xml; charset=utf-8");
					resp.getWriter().println(a.Content);
				}
				else
				{
					resp.setContentType("text/html");
					resp.getWriter().println("Sorry! The named method was not found!");
				}

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
				var p = this.req.getPathInfo();

				if (p.StartsWith("/"))
				{
					var MethodName = p.Substring(1);
					//Console.WriteLine("MethodName: " + MethodName);
					return MethodName;
				}

				return "";
			}

			public string GetString(string name)
			{
				// http://www.jguru.com/faq/view.jsp?EID=1307699
				// http://books.google.ee/books?id=_0HnEk1djCwC&pg=PA93&lpg=PA93&dq=app+engine+application/x-www-form-urlencoded&source=bl&ots=Kv2QqyAy9D&sig=eW2UWO3OwTb1YZ-8wsJBoef7nck&hl=et&ei=9mUKS_DxFpPv-QaHyOjFDQ&sa=X&oi=book_result&ct=result&resnum=5&ved=0CB8Q6AEwBDgK#v=onepage&q=app%20engine%20application%2Fx-www-form-urlencoded&f=false
				// http://java.sun.com/j2ee/1.4/docs/api/javax/servlet/ServletRequest.html#getAttribute(java.lang.String)

				var a = this.req.getAttribute(name);

				if (a == null)
					return "";

				return (string)a;
			}

			public void SetReturnParameterString(string value)
			{
				// we really should escape value for xml...

				this.Content = "<?xml version='1.0' encoding='utf-8'?><string xmlns='http://tempuri.org/'>" + value + "</string>";
			}

			public string Content;
		}

		public abstract void InvokeWebService(InvokeWebServiceArguments a);
	}
}
