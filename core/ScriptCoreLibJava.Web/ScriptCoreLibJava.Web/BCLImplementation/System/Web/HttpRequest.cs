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
				return this.InternalContext.getMethod();
			}
		}

		public NameValueCollection Form
		{
			get
			{
				return new NameValueCollection();
			}
		}

		public NameValueCollection QueryString
		{
			get
			{
				return new NameValueCollection();
			}
		}
	}
}
