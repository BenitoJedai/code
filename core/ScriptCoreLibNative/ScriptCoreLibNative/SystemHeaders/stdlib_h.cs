using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders
{
	/// <summary>
	/// http://www.unet.univie.ac.at/aix/libs/basetrf1/malloc.htm
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
