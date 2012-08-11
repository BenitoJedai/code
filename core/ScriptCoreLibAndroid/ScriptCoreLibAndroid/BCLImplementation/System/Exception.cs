using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android.BCLImplementation.System
{
    [Script(
   HasNoPrototype = true,
  Implements = typeof(global::System.Exception),
  ImplementationType = typeof(java.lang.Throwable))]
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
