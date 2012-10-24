using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(IDictionary<,>))]
    internal interface __IDictionary<TKey, TValue> : __ICollection__KeyValuePair<TKey, TValue>, __IEnumerable<KeyValuePair<TKey, TValue>>, __IEnumerable
    {
        ICollection<TKey> Keys { get; }

        ICollection<TValue> Values { get; }

        TValue this[TKey key] { get; set; }

        void Add(TKey key, TValue value);

        bool ContainsKey(TKey key);

        bool Remove(TKey key);

        // ref-out not supported yet
        //bool TryGetValue(TKey key, out TValue value);


        // java cannot base type parameter type?
        //void __TypeReference(KeyValuePair<TKey, TValue> e);
    }

    [Script]
    // todo: make sure the lookup works for this:
    // [Script(Implements = typeof(ICollection<KeyValuePair<TKey, TValue>>))]
    interface __ICollection__KeyValuePair<TKey, TValue>
    {


        // casting will be broken using this canon version
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20121001-solutionbuilderv1/20121024-linq

        bool Remove(KeyValuePair<TKey, TValue> item);

        // compiler should suport wrapping this type and exposing it as  __ICollection<__KeyValuePair<TKey, TValue>>
    }
}
