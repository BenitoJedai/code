using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLibJava.BCLImplementation.System.Web;
using System.Web;
using System.Reflection;

namespace jsc.meta.Library.Templates.Java
{
	public class Preserve
	{
		public string virtualPath;
		public string type;
	}


	internal static class PreserveInformation
	{
		public static Preserve[] GetCurrent()
		{
			return new[] { new Preserve { type = "x", virtualPath = "/x" } };
		}
	}

	[Obfuscation(Feature = "invalidmerge")]
	internal class TypelessImplementation1 : System.Web.HttpApplication
	{
		public void _1_Application_BeginRequest(object sender, EventArgs e)
		{

		}

		public void Application_BeginRequest(object sender, EventArgs e)
		{
			if (this.Request.Path == "/jsc")
			{
				var w = new StringBuilder();

				w.AppendLine("jsc!! @ " + this.Request.Path);

				var i = PreserveInformation.GetCurrent();

				w.AppendLine("length: " + i.Length);

				foreach (var item in i)
				{
					w.AppendLine("<p><a href='" + item.virtualPath + "'>" + item.type + "</a></p>");
				}

				this.Response.Write(w.ToString());

				this.Response.StatusCode = 200;
				this.Response.ContentType = "text/html";

				this.CompleteRequest();
				return;
			}

			if (this.Request.Path == "/jsc-solutions")
			{
				this.Response.Redirect("http://jsc-solutions.net");

				this.CompleteRequest();
				return;
			}

			_1_Application_BeginRequest(sender, e);
		}
	}


	public class InternalHttpServlet : javax.servlet.http.HttpServlet
	{
		// this class is a template
		// this class cannot be used in .net
		// this could be defined in ScriptCoreLib.Ultra


		public InternalHttpServlet()
		{


		}

		protected override void doPost(javax.servlet.http.HttpServletRequest req, javax.servlet.http.HttpServletResponse resp)
		{
			InternalInvokeWebService(req, resp);
		}

		protected override void doGet(javax.servlet.http.HttpServletRequest req, javax.servlet.http.HttpServletResponse resp)
		{
			InternalInvokeWebService(req, resp);
		}

		private void InternalInvokeWebService(javax.servlet.http.HttpServletRequest req, javax.servlet.http.HttpServletResponse resp)
		{
			try
			{
				//Console.WriteLine("<request>");

				TypelessImplementation1 Application = new TypelessImplementation1();
				__HttpApplication Application1;

				Application1 = (__HttpApplication)(object)Application;

				Application1.Request = (HttpRequest)(object)new __HttpRequest { InternalContext = req };
				Application1.Response = (HttpResponse)(object)new __HttpResponse { InternalContext = resp };

				Application.Application_BeginRequest(new object(), new EventArgs());

				//Console.WriteLine("</request>");

			}
			catch (csharp.ThrowableException exc)
			{
				//Console.WriteLine("error!");
				((java.lang.Throwable)(object)exc).printStackTrace();
			}
		}

	}

}
