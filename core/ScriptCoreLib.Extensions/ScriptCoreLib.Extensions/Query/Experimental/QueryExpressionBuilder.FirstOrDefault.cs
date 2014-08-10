using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ScriptCoreLib.Extensions;
using System.Reflection;
using System.IO;
using System.Data;

namespace ScriptCoreLib.Query.Experimental
{
    public static partial class QueryExpressionBuilder
    {
        partial class SQLWriter<TElement>
        {

        }


        //[Obsolete]
        //public static TElement FirstOrDefault<TElement>(this IQueryStrategy<TElement> source)
        //{
        //    // cache it?
        //    var sql = new SQLWriter<TElement>(source, new IQueryStrategy[0].AsEnumerable());


        //    return default(TElement);
        //}


        public static TElement FirstOrDefault<TElement>(this IQueryStrategy<TElement> source)
        {
            var value = default(TElement);

            WithConnection(
                cc =>
                {
                    value = FirstOrDefault(source, cc);

                }
            );

            return value;
        }

        public static TElement FirstOrDefault<TElement>(this IQueryStrategy<TElement> source, IDbConnection cc)
        {
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestXMySQL\Program.cs
            // make sure the reader will be closed
            var a = source.Take(1).AsEnumerable(cc).ToArray();
            return a.FirstOrDefault();
        }



        public static TElement Last<TKey, TElement>(this IQueryStrategyGrouping<TKey, TElement> source)
        //public static TElement Last<TElement>(this IQueryStrategy<TElement> source)
        {
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestGroupBy\Program.cs

            return default(TElement);
        }


    }

}
