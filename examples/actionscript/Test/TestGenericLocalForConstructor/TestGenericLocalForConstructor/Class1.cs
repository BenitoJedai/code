using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]
namespace TestGenericLocalForConstructor
{
    public class Class1Base<T>
    {
        public Class1Base(T t = default(T))
        {

        }
    }

    public class Class1<T> : Class1Base<T>
    {
        public Class1(T arg)
            : base(default(T))
        {
            var loc = arg;

            new Class1<T>(loc);
        }
    }

    public class Class1
    {
        public Class1(object arg)
        {
            var loc = arg;

            new Class1(loc);
        }
    }
}


namespace ScriptCoreLib.Shared.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(global::System.Collections.Generic.KeyValuePair<,>))]
    internal class __KeyValuePair<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public __KeyValuePair()
            : this(default(TKey), default(TValue))
        {

        }

        // does this work for PHP?
        public __KeyValuePair(TKey Key, TValue Value)
        {
            this.Key = Key;
            this.Value = Value;
        }


    }
}
