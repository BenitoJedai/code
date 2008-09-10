using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace DelegateToExtensionMethod.Shared
{
	[Script]
	public static class MySharedExtensions
	{

		public static Action<string> ToConcat(this string e, Action<string> h)
		{
			return a => h(e + a);
		}
	}
}
