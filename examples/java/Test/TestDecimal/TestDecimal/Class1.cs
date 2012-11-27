using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]

namespace TestDecimal
{
    public class Class1
    {
        public decimal GetDecimal(int i)
        {
            // System.Decimal GetDecimal(Int32)


            return default(decimal);
        }
    }
}

namespace ScriptCoreLibJava.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Decimal)
        //, ImplementationType = typeof(java.lang.Float)

    )]
    internal class __Decimal
    {
        // Error	1	Java : valuetype ScriptCoreLibJava.BCLImplementation.System.__Decimal - ScriptCoreLibJava.BCLImplementation.System.__Decimal must define a default .ctor	X:\jsc.svn\examples\java\Test\TestDecimal\TestDecimal\script	TestDecimal


        public __Decimal()
        {

        }

        public __Decimal(int i)
        {

        }
    }
}
