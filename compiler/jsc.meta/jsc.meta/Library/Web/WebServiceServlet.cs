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

		public class InvokeWebServiceArguments
		{
			public javax.servlet.http.HttpServletRequest req;

			public string GetMethodName()
			{
				var p = this.req.getPathInfo();

				if (p == null)
					return "";

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

			public string DocumentContent;

			internal void RenderMethodsToDocumentContent()
			{
				var a = new[] {
					"foo", 
					"bar"
				};

				RenderMethodsToDocumentContent(a);
			}

			public string ServiceName;

			public void RenderMethodsToDocumentContent(string[] Methods)
			{
				// we are server side
				// we are rendering a html document
				// we could use ScriptCoreLib.Document if it were ready for serverside DOM

				var w = new StringBuilder();

				w.AppendLine(@"

<html>

    <head><link rel='alternate' type='text/xml' href='?disco' />

    <style type='text/css'>
    
		BODY { color: #000000; background-color: white; font-family: Verdana; margin-left: 0px; margin-top: 0px; }
		#content { margin-left: 30px; font-size: .70em; padding-bottom: 2em; }
		A:link { color: #336699; font-weight: bold; text-decoration: underline; }
		A:visited { color: #6699cc; font-weight: bold; text-decoration: underline; }
		A:active { color: #336699; font-weight: bold; text-decoration: underline; }
		A:hover { color: cc3300; font-weight: bold; text-decoration: underline; }
		P { color: #000000; margin-top: 0px; margin-bottom: 12px; font-family: Verdana; }
		pre { background-color: #e5e5cc; padding: 5px; font-family: Courier New; font-size: x-small; margin-top: -5px; border: 1px #f0f0e0 solid; }
		td { color: #000000; font-family: Verdana; font-size: .7em; }
		h2 { font-size: 1.5em; font-weight: bold; margin-top: 25px; margin-bottom: 10px; border-top: 1px solid #003366; margin-left: -15px; color: #003366; }
		h3 { font-size: 1.1em; color: #000000; margin-left: -15px; margin-top: 10px; margin-bottom: 10px; }
		ul { margin-top: 10px; margin-left: 20px; }
		ol { margin-top: 10px; margin-left: 20px; }
		li { margin-top: 10px; color: #000000; }
		font.value { color: darkblue; font: bold; }
		font.key { color: darkgreen; font: bold; }
		font.error { color: darkred; font: bold; }
		.heading1 { color: #ffffff; font-family: Tahoma; font-size: 26px; font-weight: normal; background-color: #003366; margin-top: 0px; margin-bottom: 0px; margin-left: -30px; padding-top: 10px; padding-bottom: 3px; padding-left: 15px; width: 105%; }
		.button { background-color: #dcdcdc; font-family: Verdana; font-size: 1em; border-top: #cccccc 1px solid; border-bottom: #666666 1px solid; border-left: #cccccc 1px solid; border-right: #666666 1px solid; }
		.frmheader { color: #000000; background: #dcdcdc; font-family: Verdana; font-size: .7em; font-weight: normal; border-bottom: 1px solid #dcdcdc; padding-top: 2px; padding-bottom: 2px; }
		.frmtext { font-family: Verdana; font-size: .7em; margin-top: 8px; margin-bottom: 0px; margin-left: 32px; }
		.frmInput { font-family: Verdana; font-size: 1em; }
		.intro { margin-left: -15px; }
           
    </style>

    <title>
	WebService1 Web Service
</title></head>

  <body>

    <div id='content'>

      <p class='heading1'>WebService1</p><br>

      

      <span>

          <p class='intro'>The following operations are supported.  For a formal definition, please review the <a href='?WSDL'>Service Description</a>. </p>

");
				w.AppendLine("<ul>");

				foreach (var item in Methods)
				{
					w.AppendLine("<li><a href='" + this.ServiceName + ".asmx?op=" + item + "'>" + item + "</a></li>");

				}

				w.AppendLine("</ul>");


				w.AppendLine(@"

</span>
</div>
</body>
</html>
");

				this.DocumentContent = w.ToString();
			}
		}

		public abstract void InvokeWebService(InvokeWebServiceArguments a);
	}
}
