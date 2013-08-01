using ScriptCoreLib;
using java.lang;

namespace java.lang.reflect
{
    [Script(IsNative = true)]
    public class InvocationTargetException : ReflectiveOperationException
    {
        // jsc should allow base type upgrades!

        //1:00ad:22ef ScriptCoreLibJava define java.lang.reflect.InvocationTargetException
        //java.lang.reflect.InvocationTargetException, ScriptCoreLibJava, Version=4.1.0.0, Culture=neutral, PublicKeyToken=null
        //java.lang.Exception, ScriptCoreLibJava, Version=4.1.0.0, Culture=neutral, PublicKeyToken=null
        //java.lang.ReflectiveOperationException, ScriptCoreLibJava.Natives, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
        //RewriteToAssembly error: System.InvalidOperationException: Mismatch detected in base types for java.lang.reflect.InvocationTargetException


        /// <summary>
        /// Returns the the cause of this exception (the thrown target exception, which may be null).
        /// </summary>
        /// <returns></returns>
        public Throwable getCause()
        {
            return default(Throwable);
        }


        /// <summary>
        /// Get the thrown target exception.
        /// </summary>
        /// <returns></returns>
        public Throwable getTargetException()
        {
            return default(Throwable);
        }

    }
}
