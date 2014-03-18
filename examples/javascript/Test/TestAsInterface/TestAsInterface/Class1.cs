using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestAsInterface
{
    public interface IClass1
    {

    }

    public class Class1
    {
        public static void isOperator(object x)
        {
            var ic = x is Class1;
            var ii = x is IClass1;
        }

        public static void asOperator(object x)
        {
            // X:\jsc.internal.svn\compiler\jsc\Languages\JavaScript\IL2ScriptGenerator.OpCodes.Isinst.cs

            var ic = x as Class1;

            // d = ( function () { var c$12 = b.constructor; return 'Interfaces' in c$12 ? ('QJBC6GwDhzGvDLoJZoY_aAg' in c$12.Interfaces) : false; } )();
            // d = ( function () { var c$9 = b; return (c$9 instanceof QJBC6GwDhzGvDLoJZoY_aAg ? c$9 : null); } )();
            var ii = x as IClass1;
        }
    }
}
