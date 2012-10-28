using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]

namespace TestSatteliteConstructorDefaults
{
    internal class __OrderedEnumerable<TSource, TKey>
    {

        internal __OrderedEnumerable()
            : this(null, false, __Colors.Transparent)
        {

        }

        public __OrderedEnumerable(object comparer, bool descending, __Color Color)
        {

        }


    }

    internal class Class1
    {
        static void foo()
        {
            var x = new __OrderedEnumerable<object, bool>();

        }
    }

}



namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Media
{
    internal static class __Colors
    {
        public static __Color Transparent { get { throw null; } }
    }

    internal class __Color
    {
    }


}