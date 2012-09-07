using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

[assembly: Obfuscation(Feature = "script")]

namespace TestConstructorOverloadForDictionary
{
    [Script(Implements = typeof(Dictionary<,>))]
    internal class __Dictionary<TKey, TValue> 
    {
        public __Dictionary()
        {

            var x = new __Enumerator();
        }



        [Script(Implements = typeof(global::System.Collections.Generic.Dictionary<,>.Enumerator)
            //,IsDebugCode = true
          )]
        public class __Enumerator
        {
			public __Enumerator() : this(null) { }

            public __Enumerator(__Dictionary<TKey, TValue> e)
            {
                var x = new KeyValuePair<TKey, TValue>(default(TKey), default(TValue));

            }
        }
    }
}


namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(global::System.Collections.Generic.KeyValuePair<,>))]
    internal class __KeyValuePair<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        // todo: fix jsc on ctor merging



        public __KeyValuePair()
            : this(default(TKey), default(TValue))
        {

        }


        public __KeyValuePair(TKey Key, TValue Value)
        {
            this.Key = Key;
            this.Value = Value;
        }


    }
}
