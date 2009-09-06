using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;
using csharp;

namespace ScriptCoreLibJava.BCLImplementation.System
{
	[Script(
	   HasNoPrototype = true,

	  Implements = typeof(global::System.NotSupportedException),
	  ImplementationType = typeof(java.lang.RuntimeException))]
	internal class __NotSupportedException : __Exception
	{
		public __NotSupportedException()
		{


		}

		public __NotSupportedException(string m)
		{

		}
	}

}
