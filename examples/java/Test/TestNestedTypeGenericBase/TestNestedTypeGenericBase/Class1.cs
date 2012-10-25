using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]
namespace TestNestedTypeGenericBase
{
    [Script(Implements = typeof(global::System.Collections.Generic.Comparer<>))]
    internal abstract class __Comparer<T>
    {
        class __GenericComparer : __Comparer<T>
        {

        }
    }
}
