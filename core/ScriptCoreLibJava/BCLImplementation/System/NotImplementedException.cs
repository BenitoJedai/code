using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;
using csharp;

namespace ScriptCoreLibJava.BCLImplementation.System
{
    [Script(
       HasNoPrototype = true,
       
      Implements = typeof(global::System.NotImplementedException),
      ImplementationType = typeof(java.lang.RuntimeException))]
    internal class __NotImplementedException : __Exception
    {
     
    }

}
