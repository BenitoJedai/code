using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{
	[Script(InternalConstructor = true, Implements = typeof(global::System.NotSupportedException))]
	internal class __NotSupportedException : __Exception
	{
		public __NotSupportedException(string message) : base(message) { }

		//static __NotImplementedException InternalConstructor()
		//{
		//    return (__NotImplementedException)__Exception.InternalConstructor("NotImplementedException");
		//}

		internal static __NotSupportedException InternalConstructor(string e)
		{
			return (__NotSupportedException)__Exception.InternalConstructor("NotSupportedException: " + e);
		}

	}
}
