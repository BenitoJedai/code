using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ScriptCoreLib.Extensions;
using System.Reflection;

namespace ScriptCoreLib.Query.Experimental
{
    public static partial class QueryExpressionBuilder
    {
        class xTake : IQueryStrategy
        {
            public IQueryStrategy source;
            public int count;

            public override string ToString()
            {
                return "take " + count;
            }
        }

        class xTake<TElement> : xTake, IQueryStrategy<TElement>
        {

        }

        // called by LINQ
        public static IQueryStrategy<TElement> Take<TElement>(this IQueryStrategy<TElement> source, int count)
        {
            return new xTake<TElement>
            {
                source = source,
                count = count
            };
        }


    }

}
