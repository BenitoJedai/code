using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ScriptCoreLib.Extensions;
using System.Reflection;
using System.Data;
using System.Threading.Tasks;
using System.Data.Common;

namespace ScriptCoreLib.Query.Experimental
{
    public static partial class QueryExpressionBuilder
    {
        // X:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\Query\Experimental\QueryExpressionBuilderAsync.IDbConnection.Insert.cs



        class xDelete : IQueryStrategy
        {
            public IQueryStrategy source;

            public override string ToString()
            {
                return "delete";
            }
        }

        class xDelete<TElement> : xDelete, IQueryStrategy<TElement>
        {

        }





        // should be exposing IQueryStrategy instead of xSelect
        public static void Delete<TElement, TKey>(this xSelect<TKey, TElement> source, TKey key)
        {
            //source.keySelector
            // will this work for chrome too?
            // do we have enough type information available now?

            #region filter


            // Member = {TestXMySQL.PerformanceResourceTimingData2ApplicationPerformanceKey Key}
            var xMemberInitExpression = source.keySelector.Body as MemberInitExpression;

            var xFieldInfo = xMemberInitExpression.Bindings[0].Member as FieldInfo;

            var p = Expression.Parameter(
                 // xrow
                 xMemberInitExpression.Type, "x"
             );

            //Additional information: Field 'TestXMySQL.PerformanceResourceTimingData2ApplicationPerformanceRow.Key' is not defined for type 'System.Object'

            var BodyLeft = Expression.Convert(
                Expression.Field(p, xFieldInfo), // FieldExpression
                typeof(long),
                null
            );

            var BodyRight = Expression.Convert(
                Expression.Constant(key),
                typeof(long),
                null
            );

            var e = Expression.Equal(BodyLeft, BodyRight);
            var filter = Expression.Lambda(e, p);
            #endregion

            //Delete(source, filter);
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestXMySQL\Program.cs
            source.Where(filter).Delete();
        }


        // convinience method
        public static void Delete<TElement>(this IQueryStrategy<TElement> source, Expression<Func<TElement, bool>> filter)
        {
            // could we also just ask for a key?
            // can we ask a TElement to have a key field for us? generic field constraint?

            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectMath\Program.cs
            source.Where(filter).Delete();
        }


        // delete all?
        public static void Delete<TElement>(this IQueryStrategy<TElement> source)
        {
            WithConnection(
              cc =>
              {
                  Delete(source, cc);
              }
          );

        }

        public static void Delete<TElement>(this IQueryStrategy<TElement> source, IDbConnection cc)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/20140705/20140705


            // how was it done before?
            // tested by?

            var nsource = new xDelete { source = source };


            var c = (DbCommand)cc.CreateCommand();


            var w = new SQLWriter<TElement>(nsource, new IQueryStrategy[] { nsource }, Command: c);


            // Additional information: There is already an open DataReader associated with this Connection which must be closed first.
            c.ExecuteNonQuery();
        }

        // http://stackoverflow.com/questions/4471277/mysql-delete-from-with-subquery-as-condition
        // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectMath\Program.cs
        // X:\jsc.svn\examples\javascript\LINQ\GGearAlpha\GGearAlpha\Library\GoogleGearsAdvanced.cs

    }

}
