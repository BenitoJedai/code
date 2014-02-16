using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]

namespace TestStructByRefInit
{
    [Script(Implements = typeof(global::System.ValueType))]
    internal abstract class __ValueType
    {

    }

    public struct Class1
    {
        static void foo()
        {
            //var ref$b = [new a6i_be5HP_aDmwkqFk7mQ_akA()];
            //AwAABpHP_aDmwkqFk7mQ_akA(ref$b);

            //Class1[] ref_class10 = new Class1[1];
            //ref_class10[0] = new Class1();

            Class1 x;

            foo(ref x);
        }

        static void foo(ref Class1 x)
        {

        }
    }
}
