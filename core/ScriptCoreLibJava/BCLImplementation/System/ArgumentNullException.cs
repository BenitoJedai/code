using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;
//using csharp;

namespace ScriptCoreLibJava.BCLImplementation.System
{
    [Script(
       HasNoPrototype = true,
      Implements = typeof(global::System.ArgumentNullException)
      , ImplementationType = typeof(java.lang.RuntimeException)
      )]
    internal class __ArgumentNullException : __Exception
    {
        public __ArgumentNullException()
        {


        }

        public __ArgumentNullException(string m)
        {

        }
    }

}
