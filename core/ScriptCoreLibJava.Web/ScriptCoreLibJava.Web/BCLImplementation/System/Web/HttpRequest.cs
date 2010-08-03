using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Collections.Specialized;

namespace ScriptCoreLibJava.BCLImplementation.System.Web
{
	[Script(Implements = typeof(global::System.Web.HttpRequest))]
	internal class __HttpRequest
	{
		public javax.servlet.http.HttpServletRequest InternalContext;

		public string Path
		{
			get
			{
				return this.InternalContext.getPathInfo();
			}
		}

		public string HttpMethod
		{
			get
			{
				// or http://msdn.microsoft.com/en-us/library/system.web.httprequest.requesttype(VS.85).aspx?

				return this.InternalContext.getMethod();
			}
		}

		NameValueCollection InternalForm;

		public NameValueCollection Form
		{
			get
			{
				//Console.WriteLine("get_Form");

				if (InternalForm == null)
				{
                    InternalForm = new NameValueCollection();
                    InitializeForm();
				}

				//Console.WriteLine("return get_Form");

				return InternalForm;
			}
		}

        private void InitializeForm()
        {
            //Console.WriteLine("get_Form = new");


            var e = this.InternalContext.getParameterNames();

            // For HTTP servlets, parameters are contained in 
            // the query string or posted form data.

            // see: http://209.85.229.132/search?q=cache:0aKqPR_HgIUJ:g.oswego.edu/dl/classes/collections/SimpleTest.java+while+hasMoreElements&cd=1&hl=en&ct=clnk
            // see: http://java.sun.com/j2ee/1.4/docs/api/javax/servlet/ServletRequest.html#getParameter(java.lang.String)
            // see: http://java.sun.com/j2ee/1.4/docs/api/javax/servlet/http/HttpServletRequest.html#getQueryString()

            var qs = this.InternalContext.getQueryString();
            var q = new InternalQueryStringParser(qs);

            while (e.hasMoreElements())
            {
                var name = (string)e.nextElement();

                if (q[name] == null)
                {
                    var value = this.InternalContext.getParameter(name);

                    //Console.WriteLine("Form add: " + name + " = " + value);
                    InternalForm[name] = value;
                }
                else
                {
                    //Console.WriteLine("Form skip: " + name);

                }
            }
        }

		[Script]
		public class InternalQueryStringParser : NameValueCollection
		{
			// code duplication :)
			public readonly string QueryString;

			public InternalQueryStringParser(string QueryString)
			{
				if (null == QueryString)
				{
					this.QueryString = "";

					return;
				}

				this.QueryString = QueryString;

				//Console.WriteLine("InternalQueryStringParser: QueryString=" + QueryString);

				foreach (var item in QueryString.Split('&'))
				{
					var p = item.Split('=');

					if (p.Length == 2)
					{
						var value = p[0];
						var name = p[1];

						this[value] = name;

						//Console.WriteLine("InternalQueryStringParser: " + value + " = " + name);
					}

				}
			}
		}

		NameValueCollection InternalQueryString;

		public NameValueCollection QueryString
		{
			get
			{
				if (InternalQueryString == null)
				{
					InternalQueryString = new NameValueCollection();

					// For HTTP servlets, parameters are contained in 
					// the query string or posted form data.
					var e = this.InternalContext.getParameterNames();

					// see: http://209.85.229.132/search?q=cache:0aKqPR_HgIUJ:g.oswego.edu/dl/classes/collections/SimpleTest.java+while+hasMoreElements&cd=1&hl=en&ct=clnk

					var qs = this.InternalContext.getQueryString();
					var q = new InternalQueryStringParser(qs);

					while (e.hasMoreElements())
					{
						var name = (string)e.nextElement();

						if (q[name] != null)
						{
							var value = this.InternalContext.getParameter(name);

							//Console.WriteLine("QueryString add: " + name + " = " + value);
							InternalQueryString[name] = value;
						}
						else
						{
							//Console.WriteLine("QueryString skip: " + name);

						}
					}
				}


				return InternalQueryString;
			}
		}
	}
}
