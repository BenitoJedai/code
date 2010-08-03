using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Library.Delegates;
using System.Web.Profile;
using ScriptCoreLib.Ultra.Library.Extensions;
using ScriptCoreLib.Extensions;

namespace ScriptCoreLib.Ultra.WebService
{
	public static class InternalGlobalExtensions
	{
        public static InternalFileInfo ToCurrentFile(this InternalGlobal g)
        {
            var that = g.InternalApplication;

            var x = default(InternalFileInfo);
            foreach (var item in g.GetFiles())
            {
                if (that.Request.Path == "/" + item.Name)
                {
                    x = item;
                    break;
                }
            }
            return x;
        }

		public static bool FileExists(InternalGlobal g)
		{
            return g.ToCurrentFile() != null;
		}

		public static string escapeXML(string s)
		{
			return s.ToXMLString();
		}

		public static void InternalApplication_BeginRequest(InternalGlobal g)
		{
			var that = g.InternalApplication;
			var Context = that.Context;

			if (InternalGlobalExtensions.FileExists(g))
			{
				// fake lag
				//if (that.Request.Path.EndsWith(".js"))
				//    System.Threading.Thread.Sleep(1000);

				return;
			}

			if (Context.Request.Path == "/favicon.ico")
			{
                Context.Response.WriteFile("assets/ScriptCoreLib/jsc.ico");
                
				that.CompleteRequest();
				return;
			}

			if (Context.Request.Path == "/robots.txt")
			{
				Context.Response.StatusCode = 404;
				that.CompleteRequest();
				return;
			}

			if (Context.Request.Path == "/crossdomain.xml")
			{
				Context.Response.StatusCode = 404;
				that.CompleteRequest();
				return;
			}

            StringAction Write = 
                e =>
                {
                    // could we take the method pointer implicitly?
                    Context.Response.Write(e);
                };

			var WebMethods = g.GetWebMethods();

            //Console.WriteLine();

			foreach (var item in WebMethods)
			{
				item.LoadParameters(that.Context);
			}

			if (Context.Request.HttpMethod == "POST")
			{
				var WebMethod = InternalWebMethodInfo.First(WebMethods, Context.Request.QueryString[InternalWebMethodInfo.QueryKey]);
				if (WebMethod == null)
				{
					Context.Response.StatusCode = 404;
					that.CompleteRequest();
					return;
				}

				g.Invoke(WebMethod);

				if (that.Context.Request.Path == "/xml")
				{
					WriteXDocument(g, Write, WebMethod);
					that.CompleteRequest();
					return;
				}

				that.Response.ContentType = "text/html";
				WriteDiagnosticsResults(Write, WebMethod);
				WriteDiagnostics(g, Write, WebMethods);
				that.CompleteRequest();
				return;

			}

			var IsComplete = false;

			var h = new WebServiceHandler
			{
				Context = that.Context,

				CompleteRequest = delegate
				{
					IsComplete = true;
					that.CompleteRequest();
				},

				Applications = g.GetScriptApplications(),

				Default = delegate
				{
					that.Response.ContentType = "text/html";

                    // todo: jsc: PHP workaround required
                    var apps = g.GetScriptApplications();
                    var app = apps[0];

					app.WriteTo(Write);

					IsComplete = true;
					that.CompleteRequest();
				},

				Diagnostics = delegate
				{
					that.Response.ContentType = "text/html";
					WriteDiagnostics(g, Write, WebMethods);

					IsComplete = true;
					that.CompleteRequest();
				},

				Redirect = delegate
				{
					that.Response.Redirect("/#" + that.Request.Path);

					IsComplete = true;
					that.CompleteRequest();
				}
			};

	
			g.Serve(h);

			if (!IsComplete)
			{
				if (that.Request.Path == "/jsc")
				{
					h.Diagnostics();
					return;
				}

				if (h.IsDefaultPath)
				{
					h.Default();
					return;
				}


				// we could invoke web service handler now?
                h.Redirect();
                //h.Diagnostics();
			}
		}

	

		public static bool IsDefaultPathOrSpecialPath(string e)
		{
			if (WebServiceHandler.InternalIsDefaultPath(e))
				return true;

			if (e == "/jsc")
				return true;

			if (e == "/xml")
				return true;

			return false;
		}


		private static void WriteDiagnosticsResults(StringAction Write, InternalWebMethodInfo WebMethod)
		{
			if (WebMethod.Results == null)
			{

				Write("<h2>No Results</h2>");
			}
			else
			{
				Write("<h2>" + WebMethod.Results.Length + " Results</h2>");

				foreach (var item in WebMethod.Results)
				{
					WriteWebMethod(Write, item,
						Parameter =>
						{
							if (Parameter == null)
								return;

							Write(" = '<code style='color: red'>" + escapeXML(Parameter.Value) + "</code>'");
						}
					);
				}

				Write("<br />");
			}
		}

		private static void WriteDiagnostics(InternalGlobal g, StringAction Write, InternalWebMethodInfo[] WebMethods)
		{
            // should the diagnostics be a separate rich Browser Application? :)

			var Context = g.InternalApplication.Context;

            Write("<title>jsc-solutions.net</title>");

			Write("<a href='http://jsc-solutions.net'><img border='0' src='/assets/ScriptCoreLib/jsc.png' /></a>");


			Write("<h2>Special pages</h2>");

			Write("<br /> " + "<img src='http://www.favicon.cc/favicon/16/38/favicon.png' /> special page: " + "<a href='/robots.txt'>/robots.txt</a>");
			Write("<br /> " + "<img src='http://www.favicon.cc/favicon/16/38/favicon.png' /> special page: " + "<a href='/xml'>/xml</a>");
			Write("<br /> " + "<img src='http://www.favicon.cc/favicon/16/38/favicon.png' /> special page: " + "<a href='/crossdomain.xml'>/crossdomain.xml</a>");
			Write("<br /> " + "<img src='http://www.favicon.cc/favicon/16/38/favicon.png' /> special page: " + "<a href='/favicon.ico'>/favicon.ico</a>");
			Write("<br /> " + "<img src='http://www.favicon.cc/favicon/16/38/favicon.png' /> special page: " + "<a href='/jsc'>/jsc</a>");

			Write("<h2>WebMethods</h2>");



			foreach (var item in WebMethods)
			{
				WriteWebMethodForm(g, Write, item);
			}


            Write("<br /> Path: '" + Context.Request.Path + "'");
            Write("<br /> HttpMethod: '" + Context.Request.HttpMethod + "'");

			Write("<h2>Form</h2>");
			foreach (var item in Context.Request.Form.AllKeys)
			{
				Write("<br /> " + "<img src='http://i.msdn.microsoft.com/w144atby.pubproperty(en-us,VS.90).gif' /> <code>");
				Write(item);
				Write(" = ");
				Write(escapeXML(Context.Request.Form[item]));
				Write("</code>");
			}

			Write("<h2>QueryString</h2>");
			foreach (var item in Context.Request.QueryString.AllKeys)
			{
				Write("<br /> " + "<img src='http://i.msdn.microsoft.com/w144atby.pubproperty(en-us,VS.90).gif' /> <code>");
				Write(item);
				Write(" = ");
				Write(escapeXML(Context.Request.QueryString[item]));
				Write("</code>");
			}

			Write("<h2>Script Applications</h2>");

			foreach (var item in g.GetScriptApplications())
			{
				Write("<br /> " + "<img src='http://www.favicon.cc/favicon/16/38/favicon.png' /> script application: " + item.TypeName);

				foreach (var r in item.References)
				{
					Write("<br /> &nbsp;&nbsp;&nbsp;&nbsp;");

					Write("<img src='http://i.msdn.microsoft.com/yxcx7skw.pubclass(en-us,VS.90).gif' /> reference: ");
					Write(r.AssemblyFile);

				}
			}

			Write("<h2>Files</h2>");

			foreach (var item in g.GetFiles())
			{
				Write("<br /> " + "<img src='http://www.favicon.cc/favicon/16/38/favicon.png' />" + " file: <a href='" + item.Name + "'>" + item.Name + "</a>");
			}



		}

		private static void WriteXDocument(InternalGlobal g, StringAction Write, InternalWebMethodInfo WebMethod)
		{
			var that = g.InternalApplication;
			var Context = that.Context;

			Context.Response.ContentType = "text/xml";

			Write("<document>");

			if (WebMethod.Results != null)
				foreach (var item in WebMethod.Results)
				{
					Write("<" + item.Name + ">");

					foreach (var p in item.Parameters)
					{
						Write("<" + p.Name + ">");
						Write(escapeXML(p.Value));
						Write("</" + p.Name + ">");

					}

					Write("</" + item.Name + ">");

				}

			Write("</document>");

			that.CompleteRequest();
		}

		private static void WriteWebMethodForm(InternalGlobal that, StringAction Write, InternalWebMethodInfo WebMethod)
		{
			Write("<form target='_blank' action='" + WebMethod.ToQueryString() + "' method='POST'>");
			WriteWebMethod(Write, WebMethod,
				Parameter =>
				{
					if (Parameter == null)
					{
						Write("<input type='submit' value='Invoke'  />");

						return;
					}

					var key = "_" + WebMethod.MetadataToken + "_" + Parameter.Name;

					Write(" = ");
					Write("<input type='text'  name='" + key + "' value='" + Parameter.Value.Replace("'", "&apos;") + "' />");
				}
			);
			Write("</form>");
		}

		public delegate void InternalWebMethodParameterInfoAction(InternalWebMethodParameterInfo p);

		private static void WriteWebMethod(StringAction Write, InternalWebMethodInfo item, InternalWebMethodParameterInfoAction more)
		{
			if (string.IsNullOrEmpty(item.MetadataToken))
			{
				Write("<br /> ");
				Write("<img src='http://i.msdn.microsoft.com/yxcx7skw.pubdelegate(en-us,VS.90).gif' />");
				Write(" method: <code>" + item.Name + "</code>");

			}
			else
			{
				Write("<br /> <img src='http://i.msdn.microsoft.com/deshae98.pubmethod(en-us,VS.90).gif' /> method: <code><a href='" + item.ToQueryString() + "'>" + item.Name + "</a></code>");
			}

			if (more != null)
				more(null);

			if (item.Parameters != null)
				foreach (var p in item.Parameters)
				{
					Write("<br /> &nbsp;&nbsp;&nbsp;&nbsp;");

					if (p.IsDelegate)
					{
						Write("<img src='http://i.msdn.microsoft.com/yxcx7skw.pubdelegate(en-us,VS.90).gif' />");
						Write(" parameter: <code>" + p.Name + "</code>");


					}
					else
					{
						Write("<img src='http://i.msdn.microsoft.com/yxcx7skw.pubclass(en-us,VS.90).gif' />");
						Write(" parameter: <code>" + p.Name + "</code>");

						if (more != null)
							more(p);

					}

				}


		}



		public static DefaultProfile InternalGetProfile(InternalGlobal g)
		{
			var that = g.InternalApplication;
			return (DefaultProfile)that.Context.Profile;
		}

	}

}
