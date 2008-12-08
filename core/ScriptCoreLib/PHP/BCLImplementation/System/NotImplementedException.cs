using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{
	[Script(InternalConstructor = true, Implements = typeof(global::System.NotImplementedException))]
	internal class __NotImplementedException : __Exception
	{
		public __NotImplementedException(string message) : base(message) { }

		//static __NotImplementedException InternalConstructor()
		//{
		//    return (__NotImplementedException)__Exception.InternalConstructor("NotImplementedException");
		//}

		internal static __NotImplementedException InternalConstructor(string e)
		{
			return (__NotImplementedException)__Exception.InternalConstructor("NotImplementedException: " + e);
		}

	}
}
