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

namespace ScriptCoreLib.Query.Experimental
{
    public static partial class QueryExpressionBuilder
    {
        // X:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\Query\Experimental\QueryExpressionBuilderAsync.IDbConnection.Insert.cs

        #region GetInsertCommand
        public static IDbCommand GetInsertCommand<TElement>(this IQueryStrategy<TElement> source, IDbConnection cc, TElement value)
        {
            Console.WriteLine("enter Insert");

            // X:\jsc.svn\examples\javascript\Test\TestSQLiteConnection\TestSQLiteConnection\Application.cs
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectMath\Program.cs

            var xSelect = source as xSelect;

            var xMemberInitExpression = xSelect.selector.Body as MemberInitExpression;

            // insert into `PerformanceResourceTimingData2.ApplicationPerformance` (`connectStart`, `connectEnd`, `requestStart`, `responseStart`, `responseEnd`, `domLoading`, `domComplete`, `loadEventStart`, `loadEventEnd`, `EventTime`, `Tag`, `Timestamp`)  values (@connectStart


            var c = cc.CreateCommand();

            var w = new StringBuilder();

            w.AppendLine("insert into `" + xSelect.selector.Parameters[0].Name + "` (");

            //Console.WriteLine("before Bindings");

            xMemberInitExpression.Bindings.Where(SourceBinding => SourceBinding.Member.Name != "Key").WithEachIndex(
                (SourceBinding, i) =>
                {
                    if (i > 0)
                        w.Append(", ");

                    w.Append("`" + SourceBinding.Member.Name + "`");
                }
            );

            w.Append(") values (");

            xMemberInitExpression.Bindings.Where(SourceBinding => SourceBinding.Member.Name != "Key").WithEachIndex(
                  (SourceBinding, i) =>
                  {
                      if (i > 0)
                          w.Append(", ");

                      w.Append("@" + SourceBinding.Member.Name + "");

                      var f = SourceBinding.Member as FieldInfo;
                      var v = f.GetValue(value);

                      if (SourceBinding.Member.Name == "Timestamp")
                      {
                          // we are supposed to ask a signed security timestamp from HSM.

                          var now = DateTime.Now;
                          v = now;
                      }

                      //Console.WriteLine("before AddParameter");

                      c.AddParameter(
                          ParameterName: "@" + SourceBinding.Member.Name + "",
                          Value: v
                       );
                  }
              );

            w.Append(")");
            c.CommandText = w.ToString();

            return c;
        }
        #endregion

        //public static Task InsertAsync<TElement>(this IQueryStrategy<TElement> source, IDbConnection cc, TElement value)
        //{
        //    // in CLR and in browser this would work.

        //    var c = GetInsertCommand(source, cc, value) as System.Data.SQLite.SQLiteCommand;
        //    var n = c.ExecuteNonQueryA();
        //}

        public static void Insert<TElement>(this IQueryStrategy<TElement> source, IDbConnection cc, TElement value)
        {
            var c = GetInsertCommand(source, cc, value);
            var n = c.ExecuteNonQuery();
            //var nKey = cc.


            //                public const string CreateCommandText = @"create table 
            // if not exists `PerformanceResourceTimingData2.ApplicationPerformance` (
            // `Key` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
            // `connectStart` BIGINT NOT NULL, 
            // `connectEnd` BIGINT NOT NULL, 
            // `requestStart` BIGINT NOT NULL, 
            // `responseStart` BIGINT NOT NULL, 
            // `responseEnd` BIGINT NOT NULL, 
            // `domLoading` BIGINT NOT NULL, 
            // `domComplete` BIGINT NOT NULL, 
            // `loadEventStart` BIGINT NOT NULL, 
            // `loadEventEnd` BIGINT NOT NULL, 
            // `EventTime` BIGINT NOT NULL, 
            // `Tag` TEXT, 
            // `Timestamp` BIGINT NOT NULL)";
        }

        
    }

}
