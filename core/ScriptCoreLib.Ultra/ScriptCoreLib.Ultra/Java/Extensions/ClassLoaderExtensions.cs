using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using java.lang;
using ScriptCoreLibJava.Extensions;

namespace ScriptCoreLib.Java.Extensions
{
    public static class ClassLoaderExtensions
    {
        public static Type GetType(this ClassLoader loader, string TypeFullName)
        {
            var c = default(Class);

            try
            {
                c = loader.loadClass(TypeFullName);
            }
            catch (csharp.ThrowableException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("error: Unable to load: " + TypeFullName);

                ((java.lang.Throwable)(object)ex).printStackTrace();

                throw new InvalidOperationException();
            }

            return c.ToType();
        }
    }
}
