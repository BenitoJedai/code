using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestAnonymousFields
{
    public class Class1
    {
        ScriptCoreLib.Shared.IAssemblyReferenceToken ref0;

        public static object foo()
        {
            return new { frame = "none" };

        }
    }
}
