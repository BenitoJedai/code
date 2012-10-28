using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]

namespace TestExplicitGenericInterface
{
    [ScriptAttribute.ExplicitInterface]
    internal interface __ICollection : __IEnumerable
    {
        void foo();
    }

    [ScriptAttribute.ExplicitInterface]
    internal interface __IEnumerable
    {
        void foo();
    }

    [ScriptAttribute.ExplicitInterface]
    internal interface __IEnumerable<T> : __IEnumerable
    {
        void foo();


    }

    [ScriptAttribute.ExplicitInterface]
    internal interface __ICollection<T> : __IEnumerable<T>, __IEnumerable
    {
        void foo();

    }

    [ScriptAttribute.ExplicitInterface]
    internal interface __IDictionary<TKey, TValue> : __ICollection<__KeyValuePair<TKey, TValue>>, __IEnumerable<__KeyValuePair<TKey, TValue>>, __IEnumerable
    {
        void foo();
        void foo(object e);
    }

    internal class __Dictionary<TKey, TValue> : __IDictionary<TKey, TValue>
        //, __ICollection<object>
    {
        public void foo(object e)
        {
        
        }

        void __IDictionary<TKey, TValue>.foo()
        {
        }

        void __ICollection<__KeyValuePair<TKey, TValue>>.foo()
        {
        }

        void __IEnumerable<__KeyValuePair<TKey, TValue>>.foo()
        {
        }

        void __IEnumerable.foo()
        {
        }

        //void __ICollection<object>.foo()
        //{
        //}

        //void __IEnumerable<object>.foo()
        //{
        //}
    }

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
