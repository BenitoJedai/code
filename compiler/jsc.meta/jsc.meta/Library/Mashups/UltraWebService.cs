using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Delegates;

namespace jsc.meta.Library.Mashups
{
	internal static class UltraWebServiceExtensions
	{
		public static void Method1(this string input1, StringAction e)
		{
			// there is no state at the moment
			new UltraWebService().Method1(input1, e);
		}
	}

	internal sealed partial class UltraWebService
	{
		// web services could also support "interned/internal" delegates

		public void Method1(string input, StringAction yield)
		{
			// running in .net, GAEJava, php


		}
	}
}
