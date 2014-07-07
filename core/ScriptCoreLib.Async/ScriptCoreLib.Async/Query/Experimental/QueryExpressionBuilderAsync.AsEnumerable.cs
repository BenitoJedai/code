using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ScriptCoreLib.Extensions;
using System.Reflection;
using System.Data;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System.Data.Common;
using System.Threading.Tasks;

namespace ScriptCoreLib.Query.Experimental
{
    public static partial class QueryExpressionBuilderAsync
    {


        public static Task<IEnumerable<TElement>> AsEnumerableAsync<TElement>(this IQueryStrategy<TElement> source)
        {
            // X:\jsc.svn\examples\javascript\LINQ\LINQWebCamAvatars\LINQWebCamAvatars\Application.cs

            var z = new TaskCompletionSource<IEnumerable<TElement>>();

            // was it manually set?
            QueryExpressionBuilder.WithConnection(
                (IDbConnection cc) =>
                {
                    AsEnumerableAsync(source, cc).ContinueWithResult(z.SetResult);
                }
            );
            return z.Task;
        }

        public static async Task<IEnumerable<TElement>> AsEnumerableAsync<TElement>(this IQueryStrategy<TElement> source, IDbConnection cc)
        {
            Console.WriteLine("enter AsEnumerable");
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectMath\Program.cs

            var c = QueryExpressionBuilder.GetSelectCommand(source, cc);


            Console.WriteLine("before ExecuteReader");
            // this wont work for chrome?
            var r = await c.ExecuteReaderAsync();
            Console.WriteLine("after ExecuteReader");

            return QueryExpressionBuilder.ReadToElements(r, source);
        }



    }

}
