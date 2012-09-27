using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]

namespace ScriptCoreLib.Shared.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(global::System.Collections.Generic.IEnumerable<>))]
    internal interface __IEnumerable<T>
    {
        IEnumerator<T> GetEnumerator();
    }
}


namespace TestGenericInterfaceInheritance
{
    internal class __List<T> : IEnumerable<T>
    {

        public IEnumerator<T> GetEnumerator()
        {
            throw null;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw null;
        }
    }

    internal class __Dictionary<TKey, TValue>
    {
        public class KeyCollection : __List<TKey>
        {
        }

    }
}
