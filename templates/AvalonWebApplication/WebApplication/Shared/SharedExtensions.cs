using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace WebApplication.Shared
{
	[Script]
	public static class SharedExtensions
	{
		// extensions defined here shall support all languages including:
		// actionscript,
		// javascript
		// php
		// java

		public static string WithBranding(this string text)
		{
			return text + " - powered by jsc";
		}
	}
}
