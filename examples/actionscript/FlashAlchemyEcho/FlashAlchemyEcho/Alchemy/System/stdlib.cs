using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlashAlchemyEcho.Alchemy.System
{
	/// <summary>
	/// http://www.opengroup.org/onlinepubs/009695399/functions/malloc.html
	/// </summary>
	/// 
	[Script(IsNative = true, Header = "stdlib.h", IsSystemHeader = true)]
	public static class stdlib_h
	{
		public static object realloc(object ptr, int size)
		{
			return default(object);
		}

		public static void free(object e)
		{

		}
	}
}
