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
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\JVMCLRSyntaxOrderByThenGroupBy\Program.cs

            [Script(DefineAsStatic = true)]
            get
            {
                var e = (object)this as java.lang.Throwable;

                //  throwable0 = ((((Object)that) instanceof  Throwable) ? (Throwable)((Object)that) : (Throwable)null);
                if (e == null)
                    return "Message: not Throwable?";

                return e.getMessage();
            }
        }

        public virtual string StackTrace
        {
            [Script(DefineAsStatic = true)]
            get
            {
                var e = (object)this as java.lang.Throwable;

                if (e == null)
                    return "StackTrace: not Throwable?";

                var ww = new java.io.StringWriter();

                e.printStackTrace(new java.io.PrintWriter(ww));

                return ww.ToString();
            }

        }


    }




}
