using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]
namespace TestMethodOverloadGenericErasure
{
    interface __ICollection<T>
    {
        bool Remove(T item);
    }

    interface __ICollection__KeyValuePair<TKey, TValue> 
    {
        // casting will be broken using this canon version
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20121001-solutionbuilderv1/20121024-linq

        bool Remove(__KeyValuePair<TKey, TValue> item);
    }

    interface __IDictionary<TKey, TValue> : __ICollection__KeyValuePair<TKey, TValue>
    {
        bool Remove(TKey key);
    }



    class __Dictionary<TKey, TValue> :  __IDictionary<TKey, TValue>
    {
        // 1>  X:\jsc.svn\examples\java\Test\TestMethodOverloadGenericErasure\TestMethodOverloadGenericErasure\bin\Debug\web\java\TestMethodOverloadGenericErasure\__Dictionary_2.java:9: 
        // name clash: 
        // Remove(TKey) in TestMethodOverloadGenericErasure.__Dictionary_2<TKey,TValue> and 
        // Remove(T) in TestMethodOverloadGenericErasure.__ICollection_1<TestMethodOverloadGenericErasure.__KeyValuePair_2<TKey,TValue>> have the same erasure, yet neither overrides the other
        // 1>  X:\jsc.svn\examples\java\Test\TestMethodOverloadGenericErasure\TestMethodOverloadGenericErasure\bin\Debug\web\java\TestMethodOverloadGenericErasure\__Dictionary_2.java:9: name clash: Remove(java.lang.Object) in TestMethodOverloadGenericErasure.__Dictionary_2<TKey,TValue> and Remove(T) in TestMethodOverloadGenericErasure.__ICollection_1<TestMethodOverloadGenericErasure.__KeyValuePair_2<TKey,TValue>> have the same erasure, yet neither overrides the other

      
           public bool Remove(__KeyValuePair<TKey, TValue> t)
        {
            return false;
        }
        public bool Remove(TKey key)
        {
            return false;
        }

    }

    class __KeyValuePair<TKey, TValue>
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
