using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
    [Script(
       HasNoPrototype = true,
      Implements = typeof(global::System.Exception),
      ImplementationType = typeof(java.lang.Exception))]
    internal class __Exception
    {
        public __Exception() { }
        public __Exception(string e) { }
        public string Message
        {
            [Script(ExternalTarget = "getMessage")]
            get { return default(string); }
        }
    }

    [Script(
       HasNoPrototype = true,
        // Use 'ImplementationType' instead of 'ExternalTarget'
        // ExternalTarget = "java.lang.RuntimeException",
       //Implements = typeof(csharp.RuntimeException),
       Implements = typeof(csharp.RuntimeException),
       ImplementationType = typeof(java.lang.RuntimeException))]
    internal class __RuntimeException : __Exception
    {
        public __RuntimeException() { }
        public __RuntimeException(string e) { }

    }


}
