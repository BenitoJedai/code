using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Collections.Specialized
{
    // http://referencesource.microsoft.com/#System/compmod/system/collections/specialized/stringdictionary.cs
    // https://github.com/dotnet/corefx/blob/master/src/System.Collections.Specialized/src/System/Collections/Specialized/StringDictionary.cs

    [Script(Implements = typeof(global::System.Collections.Specialized.StringDictionary))]
    internal class __StringDictionary : IEnumerable
    {
        readonly Dictionary<string, string> InternalValue = new Dictionary<string, string>();


        public __StringDictionary()
        {

        }

        public IEnumerator GetEnumerator()
        {
            return this.InternalValue.GetEnumerator();
        }

        public virtual bool ContainsKey(string key)
        {
            return this.InternalValue.ContainsKey(key);
        }

        public virtual ICollection Keys
        {
            get
            {
                return InternalValue.Keys;
            }
        }

        public virtual string this[string key]
        {
            get
            {
                if (this.InternalValue.ContainsKey(key))
                    return InternalValue[key];

                return null;
            }
            set
            {
                InternalValue[key] = value;
            }
        }

        public virtual int Count
        {
            get
            {
                return this.InternalValue.Count;
            }
        }

        public virtual void Add(string key, string value)
        {
            this.InternalValue.Add(key, value);
        }

        public virtual void Clear()
        {
            this.InternalValue.Clear();
        }
    }

}
