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
	internal class IOExceptionImpl : __Exception
	{
		public IOExceptionImpl()
		{
		}

		public IOExceptionImpl(string e)
		{

		}


	}
}
