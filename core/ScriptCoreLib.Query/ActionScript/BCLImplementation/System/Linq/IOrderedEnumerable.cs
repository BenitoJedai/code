using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Linq
{
    [Script(Implements = typeof(global::System.Linq.IOrderedEnumerable<>))]
    internal interface __IOrderedEnumerable<TElement> : IEnumerable<TElement>
    {
        IOrderedEnumerable<TElement> CreateOrderedEnumerable<TKey>(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending);

    }
}
