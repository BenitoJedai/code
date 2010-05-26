using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Library.Delegates;
namespace ScriptCoreLib.Ultra.WebService
{
	public class WebServiceScriptApplication
	{
		public string TypeName;
		public string TypeFullName;

		public class Reference
		{
			public string AssemblyFile;
		}

		public Reference[] References;

		public string PageSource;

		public void WriteTo(StringAction Write)
		{
			var app = this;

			// http://validator.w3.org/check?uri=

			StringAction WriteLine = k => Write(k + Environment.NewLine);

			// this function is running in .net, google app engine java and php
			// this function is based on JavaScript.EntrypointProvider
			// we could show a cool loading animation?
			// can we have XElement support already`?

			// <html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
			WriteLine(@"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">");
			WriteLine(@"<html>");
			WriteLine(@"<head>");

			// do we need this?
			//WriteLine(@"<meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />");
			WriteLine(@"<title>" + this.TypeFullName + "</title>");

			//WriteLine("<meta name='google-site-verification' content='uMipBZ74jD_65lTkiAVKRHM1HSJRo_NAgpk6NChQuOA' />");

			//WriteLine(@"<script></script>");
			WriteLine(@"</head>");

			if (string.IsNullOrEmpty(this.PageSource))
			{
				WriteDefaultPageSource(WriteLine);
			}
			else
			{
				Write(this.PageSource);
			}


			WriteLine(@"<script type='text/xml' class='" + app.TypeName + "'></script>");

			foreach (var item in app.References)
			{
				Write(@"<script type='text/javascript' src='" + item.AssemblyFile + @".js'></script>");

			}

			WriteLine(@"</html>");
		}

		private static void WriteDefaultPageSource(StringAction WriteLine)
		{
			WriteLine(@"<body style='margin: 0; overflow: hidden;'><noscript>ScriptApplication cannot run without JavaScript!</noscript>");

			// should we display custom logo?
			// only the first image will be fetched, then the script...
			//WriteLine(@"<div style='border-style: none; position: absolute; left: 50%; top: 50%;' >");
			//WriteLine(@"<img class='LoadingAnimation' src='/assets/ScriptCoreLib/jsc.png' title='jsc' style='border-style: none; margin-left: -48px; margin-top: -48px; ' /> ");
			//WriteLine(@"</div>");

			// http://www.ajaxload.info/
			WriteLine(@"<div style='border-style: none; position: absolute; left: 50%; top: 50%;' >");
			WriteLine(@"<img class='LoadingAnimation' src='/assets/ScriptCoreLib/loading.gif' title='loading...'  style='border-style: none; margin-left: -16px; margin-top: -16px; ' /> ");
			WriteLine(@"</div>");



			WriteLine(@"</body>");
		}


	}
}
