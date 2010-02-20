using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Reflection;

namespace jsc.meta.Commands.Rewrite
{
	partial class RewriteToJavaScriptDocument
	{
		[Obfuscation(Feature = "invalidmerge")]
		public static class AsService
		{
			/// <summary>
			/// This method is to be used within ASP.NET Web Application
			/// to enable Ultra Application features, where javascript, flash, java and
			/// serverside will have seamless integration.
			/// 
			/// jsc. meta could recompile that ASP.NET Web Application at a later
			/// stage for other platforms. Other platforms cannot run jsc at runtime
			/// thus this method need to be erased and replaced with the actual results.
			/// </summary>
			/// <param name="Context"></param>
			/// <param name="PrimaryApplication"></param>
			public static void Bind(System.Web.HttpApplication Context, Type PrimaryApplication)
			{

			}

		}
	}
}
