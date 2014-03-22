extern alias jvm;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jvm::java.lang;
using jvm::ScriptCoreLibJava.Extensions;

namespace ScriptCoreLib.Java.Extensions
{
    public static class ClassLoaderExtensions
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140322

        public static Type GetType(this InternalURLClassLoader loader, string TypeFullName)
        {
            var c = default(Class);

            try
            {
                c = loader.loadClass(TypeFullName);
            }
            catch (jvm::csharp.ThrowableException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("error: Unable to load: " + TypeFullName);

                ((jvm::java.lang.Throwable)(object)ex).printStackTrace();

                throw new InvalidOperationException();
            }

            return BCLImplementationExtensions.ToType(c);
        }
    }
}
