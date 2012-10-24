using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;
//using csharp;

namespace ScriptCoreLibJava.BCLImplementation.System
{
    [Script(
       HasNoPrototype = true,
      Implements = typeof(global::System.ArgumentException)
      , ImplementationType = typeof(java.lang.RuntimeException)
      )]
    internal class __ArgumentException : __Exception
    {
        public __ArgumentException()
        {


        }

        public __ArgumentException(string m)
        {

        }
    }

}
