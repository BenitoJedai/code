using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibAppJet.AppJet;

namespace ScriptCoreLibAppJet
{
	[Script(HasNoPrototype = true)]
	public static class Native
	{
		[Script(ExternalTarget = "page")]
		static public Page page;

		[Script(ExternalTarget = "request")]
		static public Request request;

		[Script(ExternalTarget = "response")]
		static public Response response;


		[Script(ExternalTarget = "storage")]
		static public Storage storage;

		[Script(OptimizedCode = "print(html(p));")]
		public static void printHTML(string p)
		{
		}

		[Script(OptimizedCode = "print(p);")]
		public static void print(object p)
		{
		}

		[Script(OptimizedCode = "import(p);")]
		public static void import(object p)
		{
		}
	}
}
