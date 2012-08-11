using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
    [Script(
       HasNoPrototype = true,
      Implements = typeof(global::System.Exception),
      ImplementationType = typeof(java.lang.Throwable))]
      //ImplementationType = typeof(java.lang.Exception))]
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

 


}
