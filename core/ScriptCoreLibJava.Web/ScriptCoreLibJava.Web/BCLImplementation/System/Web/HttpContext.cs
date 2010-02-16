using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Web;
using System.Web.Profile;

namespace ScriptCoreLibJava.BCLImplementation.System.Web
{
	[Script(Implements = typeof(global::System.Web.HttpContext))]
	internal class __HttpContext : __IServiceProvider
	{
		public HttpRequest Request { get; set; }
		public HttpResponse Response { get; set; }

		public ProfileBase Profile { get; set; }
	
	}
}
