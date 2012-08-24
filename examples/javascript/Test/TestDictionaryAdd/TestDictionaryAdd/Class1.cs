using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

[assembly: Obfuscation(Feature = "script")]


namespace AvalonUgh.Code.Editor
{
    using ScriptCoreLib.Shared.Lambda;

    partial class Level
    {
        public sealed class AttributeDictonary : Dictionary<string, object>
        {
            public void Add(Attribute e)
            {
                var i = new KeyValuePair<string,object>("", null);

                this.Add(i);
            }
        }
    }
}

namespace ScriptCoreLib.Shared.Lambda
{
    public static class LambdaExtensions
    {
        public static void Add<TKey, TValue>(this IDictionary<TKey, TValue> e, KeyValuePair<TKey, TValue> value)
        {
            e.Add(value.Key, value.Value);
        }
    }
}

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(IDictionary<,>))]
    internal interface __IDictionary<TKey, TValue> //: ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
    {
        //ICollection<TKey> Keys { get; }

        //ICollection<TValue> Values { get; }

        //TValue this[TKey key] { get; set; }

        void Add(TKey key, TValue value);

        //bool ContainsKey(TKey key);

        //bool Remove(TKey key);

        //bool TryGetValue(TKey key, out TValue value);
    }
}

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic
{
    //using ScriptCoreLib.JavaScript.Runtime;

    [Script(Implements = typeof(global::System.Collections.Generic.Dictionary<,>))]
    internal class __Dictionary<TKey, TValue> : IDictionary<TKey, TValue>//, IEnumerable
    {


        public void Add(TKey key, TValue value)
        {
            throw null;
        }

        public bool ContainsKey(TKey key)
        {
            throw null;
        }

        public ICollection<TKey> Keys
        {
            get { throw null; }
        }

        public bool Remove(TKey key)
        {
            throw null;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            throw null;
        }

        public ICollection<TValue> Values
        {
            get { throw null; }
        }

        public TValue this[TKey key]
        {
            get
            {
                throw null;
            }
            set
            {
                throw null;
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw null;
        }

        public void Clear()
        {
            throw null;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            throw null;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw null;
        }

        public int Count
        {
            get { throw null; }
        }

        public bool IsReadOnly
        {
            get { throw null; }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw null;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw null;
        }

        global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
        {
            throw null;
        }
    }

}


namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(global::System.Collections.Generic.KeyValuePair<,>))]
    internal class __KeyValuePair<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public __KeyValuePair()
        {

        }

        public __KeyValuePair(TKey Key, TValue Value)
        {
            this.Key = Key;
            this.Value = Value;
        }
    }
}
