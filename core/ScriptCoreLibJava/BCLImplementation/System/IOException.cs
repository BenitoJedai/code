using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
	[Script(
		// why are we using this parameter?
	   HasNoPrototype = true,
		// ExternalTarget = "java.io.IOException",
	   Implements = typeof(global::System.IO.IOException),
	   ImplementationType = typeof(java.io.IOException))]
	internal class __IOExceptionImpl : __Exception
	{
		public __IOExceptionImpl()
		{
		}

		public __IOExceptionImpl(string e)
		{

		}


	}
}
