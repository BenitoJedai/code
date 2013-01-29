using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]
namespace TestStaticMethodExternalTarget
{
    [Script(HasNoPrototype = true, ExternalTarget = "qr")]
    public class ____qr
    {
        //[Script(OptimizedCode = "return qr.image(e);")]
        [Script(ExternalTarget = "qr.image")]
        internal static object image(object e)
        {
            return null;
        }
    }

    public class Class1
    {
        static void foo()
        {
            var x = ____qr.image(null);

        }
    }
}
