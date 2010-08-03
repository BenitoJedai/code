using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.PHP.Runtime;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Collections.Specialized
{
    [Script(Implements = typeof(global::System.Collections.Specialized.NameValueCollection))]
    internal class __NameValueCollection
    {
        IArray InternalCollection = new IArray();

        public virtual void Add(string name, string value)
        {
            this[name] = value;
        }

        public virtual string[] AllKeys
        {
            get
            {
                return (string[])(object)this.InternalCollection.Keys;
            }
        }

        public string this[string name]
        {
            get
            {
                return (string)this.InternalCollection[name];
            }
            set
            {
                this.InternalCollection[name] = value;
            }
        }

        public virtual void Clear()
        {
            InternalCollection = new IArray();
        }
    }
}
