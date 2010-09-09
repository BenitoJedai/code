using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(global::System.Collections.Generic.KeyValuePair<,>))]
    internal class __KeyValuePair<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        // todo: fix jsc on ctor merging



        //public __KeyValuePair()
        //    : this(default(TKey), default(TValue))
        //{

        //}


        public __KeyValuePair(TKey Key, TValue Value)
        {
            this.Key = Key;
            this.Value = Value;
        }

        public static __KeyValuePair<TKey, TValue> InternalTypeDefault()
        {
            // this is a hack.
            return new __KeyValuePair<TKey, TValue>(
                default(TKey),
                default(TValue)
            );
        }
    }
}
