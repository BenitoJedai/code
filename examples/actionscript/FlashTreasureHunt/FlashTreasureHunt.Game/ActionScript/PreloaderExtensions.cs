using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlashTreasureHunt.ActionScript
{
	[Script]
	static  class PreloaderExtensions
	{
		public static object CreateInstance(this Type e)
		{
			return Activator.CreateInstance(e);
		}

	}
}
