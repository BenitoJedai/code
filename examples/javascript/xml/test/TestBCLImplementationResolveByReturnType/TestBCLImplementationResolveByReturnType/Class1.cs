using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

[assembly: Obfuscation(Feature = "script")]

namespace TestBCLImplementationResolveByReturnType
{
    [Script(Implements = typeof(XAttribute))]
    internal class __XAttribute // : __XObject
    {
        public static explicit operator int (__XAttribute attribute)
        {
            return 33;
        }

        public static explicit operator bool (__XAttribute attribute)
        {
            return default(bool);

        }
    }

    // X:\jsc.svn\examples\javascript\xml\test\TestXAttributeOp\TestXAttributeOp\Application.cs
    public class Class1
    {
        static void Invoke(XAttribute a)
        {
            var xboolean = (bool)a;
            var xint32 = (int)a;
        }
    }
}
