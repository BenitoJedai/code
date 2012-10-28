using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]



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

        public virtual string StackTrace
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return null;
            }

        }
    }




}

namespace java.lang
{
    // http://java.sun.com/j2se/1.4.2/docs/api/java/lang/Throwable.html
    [Script(IsNative = true)]
    public class Throwable
    {
        /// <summary>
        /// Returns the detail message string of this throwable.
        /// </summary>
        /// <returns></returns>
        public string getMessage()
        {
            return default(string);
        }

        public void printStackTrace()
        {

        }

    


    }
}

