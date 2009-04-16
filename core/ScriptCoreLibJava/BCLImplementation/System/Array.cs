using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.Array), IsArray = true)]
	internal class __Array
	{
		public static void Copy(__Array sourceArray, __Array destinationArray, int length)
		{
			java.lang.JavaSystem.arraycopy(sourceArray, 0, destinationArray, 0, length);
		}

		public static void Copy(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length)
		{
			java.lang.JavaSystem.arraycopy(sourceArray, sourceIndex, destinationArray, destinationIndex, length);
		}
	}
}
