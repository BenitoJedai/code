using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibAppJet.JavaScript.AppJet
{
	// http://appjet.com/docs/librefbrowser?page=utilities&a=HttpResponse
	[Script(HasNoPrototype = true)]
	public class HttpResponse
	{
		public int status;
		public string statusInfo;
		public string data;
		public string contentType;
		public object headers;
		
	}
}
