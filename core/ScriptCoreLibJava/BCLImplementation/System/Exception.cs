using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
    [Script(
       HasNoPrototype = true,
      Implements = typeof(global::System.Exception),
      ImplementationType = typeof(java.lang.Throwable))
    ]
    internal class __Exception
    {
        public __Exception() { }
        public __Exception(string e) { }
        public __Exception(string message, Exception innerException) { }

        [Script(DefineAsStatic = true)]
        new public Type GetType()
        {
            return __Type.GetTypeFromValue(this);
        }


        public string Message
        {
            [Script(DefineAsStatic = true)]
            get
            {
                var e = (object)this as java.lang.Throwable;

                return e.getMessage();
            }
        }

        public virtual string StackTrace
        {
            [Script(DefineAsStatic = true)]
            get
            {
                var e = (object)this as java.lang.Throwable;

                var ww = new java.io.StringWriter();

                e.printStackTrace(new java.io.PrintWriter(ww));

                return ww.ToString();
            }

        }


    }




}
