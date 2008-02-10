using ScriptCoreLib;
using ScriptCoreLib.Shared;

using global::System.Collections;
using global::System.Collections.Generic;

using IDisposable = global::System.IDisposable;
using System.Linq;
using System;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Runtime;

namespace ScriptCoreLib.JavaScript.Query
{
    [Script]
    class DefaultComparer<T> : IComparer<T>
    {
        public int Compare(T ka, T kb)
        {
            var r = -2;

            if (Expando.Of(ka).IsString)
                r = Expando.Compare(ka, kb);

            if (Expando.Of(ka).IsNumber)
                r = Expando.Compare(ka, kb);

            if (Expando.Of(ka).IsBoolean)
                r = Expando.Compare(ka, kb);


            if (r == -2)
                throw new NotSupportedException();

            return r;
        }
    }

    [Script(Implements = typeof(ScriptCoreLib.Shared.Query.LocalInternalEnumerable))]
    internal static partial class LocalInternalEnumerableImplementation
    {
        public static IComparer<TKey> GetDefaultComparer<TKey>()
        {
            return new DefaultComparer<TKey>();
        }

    }
}
