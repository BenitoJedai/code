using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ScriptCoreLib.Ultra.WebService
{
	public class WebServiceHandler
	{
		public HttpContext Context;

		public Action CompleteRequest;


		public Action Diagnostics;
		public Action Default;
		public Action Redirect;
	}
}
