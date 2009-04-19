using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibAppJet.JavaScript.AppJet;

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

		/// <summary>
		/// Fetches the text of a URL and returns it as a string.
		/// </summary>
		/// <param name="url"></param>
		/// <param name="p"></param>
		/// <param name="?"></param>
		[Script(OptimizedCode = "return wget(url, p, options);")]
		public static HttpResponse wget(string url, object p, WebRequestOptions options)
		{
			return default(HttpResponse);
		}
	}

	[Script, Serializable]
	public sealed class WebRequestOptions
	{
		public object headers;
		public bool followRedirects;
		public bool complete = true;
	}
}
