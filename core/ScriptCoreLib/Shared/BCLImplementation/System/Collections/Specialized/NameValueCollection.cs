using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Collections.Specialized;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Collections.Specialized
{
    // http://referencesource.microsoft.com/#System/compmod/system/collections/specialized/namevaluecollection.cs
    // https://github.com/dotnet/corefx/blob/master/src/System.Collections.Specialized/src/System/Collections/Specialized/NameValueCollection.cs

    [Script(Implements = typeof(global::System.Collections.Specialized.NameValueCollection))]
    public class __NameValueCollection : __NameObjectCollectionBase
    {
        // http://stackoverflow.com/questions/3001108/namevaluecollection-vs-dictionarystring-string

        // http://www.jguru.com/faq/view.jsp?EID=430247
        //readonly global::java.util.HashMap InternalCollection = new global::java.util.HashMap();
        readonly StringDictionary InternalCollection = new StringDictionary();


        public virtual void Add(string name, string value)
        {
            //InternalCollection.put(name, value);
            InternalCollection.Add(name, value);
        }

        public override int Count
        {
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Net\WebClient.cs
            get
            {
                return this.InternalCollection.Count;
            }
        }

        public virtual string[] AllKeys
        {
            get
            {
                //var x = InternalCollection.keySet();

                //return (string[])x.toArray(new string[x.size()]);

                var k = new string[this.InternalCollection.Count];

                var i = -1;
                foreach (string item in this.InternalCollection.Keys)
                {
                    i++;
                    k[i] = item;
                }

                return k;
            }
        }
        public string this[string name]
        {
            get
            {
                //return (string)InternalCollection.get(name);

                return this.InternalCollection[name];
            }
            set
            {
                //InternalCollection.put(name, value);

                this.InternalCollection[name] = value;
            }
        }

        public virtual void Clear()
        {
            //InternalCollection.clear();

            this.InternalCollection.Clear();
        }
    }
}
