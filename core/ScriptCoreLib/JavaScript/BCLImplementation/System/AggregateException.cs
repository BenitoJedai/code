using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
	[Script(InternalConstructor = true, Implements = typeof(global::System.AggregateException))]
	internal class __AggregateException : __Exception
	{
		public __AggregateException(string message) { }

		static __AggregateException InternalConstructor(string e)
		{
			return (__AggregateException)__Exception.InternalConstructor("AggregateException: " + e);
		}

	}
}
