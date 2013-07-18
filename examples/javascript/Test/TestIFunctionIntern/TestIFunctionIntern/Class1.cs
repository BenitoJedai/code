using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestIFunctionIntern
{
    [Script(HasNoPrototype = true, ExternalTarget = "Function")]
    public class IFunction
    {
        public IFunction(string arg0, string body)
        {

        }
    }

    [Script]
    public class Application
    {
        public Application()
        {
            var xx = "return e;";

            var fx = new IFunction("e", xx);
            var f = new IFunction("e", "return e;");

            // before:  b = new Function('e', 'return e;');
        }
    }
}
