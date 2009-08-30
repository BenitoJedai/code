using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;
using csharp;

namespace ScriptCoreLibJava.BCLImplementation.System
{
    [Script(
       HasNoPrototype = true,
       
      Implements = typeof(global::System.InvalidOperationException),
      ImplementationType = typeof(java.lang.RuntimeException))]
	internal class __InvalidOperationException : __Exception
    {
		public __InvalidOperationException()
		{


		}

		public __InvalidOperationException(string m)
		{

		}
    }

}
