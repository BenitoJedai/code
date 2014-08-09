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
using System.Xml.Linq;

namespace ScriptCoreLib.Query.Experimental
{
    public static partial class QueryExpressionBuilder
    {
        // X:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\Query\Experimental\QueryExpressionBuilderAsync.IDbConnection.Insert.cs

        #region GetInsertCommand
        public static IDbCommand GetInsertCommand<TElement>(this IQueryStrategy<TElement> source, IDbConnection cc, TElement value)
        {
            //Console.WriteLine("enter GetInsertCommand");

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



                      // whats the type?
                      // this wont work in th browser

                      var xXElement = v as XElement;
                      if (xXElement != null)
                      {
                          // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectMath\Program.cs
                          // half way there?
                          // v = "PGdvbz5mb288L2dvbz4="

                          // binary encrypted xml 7z?
                          v =
                              ScriptCoreLib.Library.StringConversions.UTF8ToBase64StringOrDefault(
                                ScriptCoreLib.Library.StringConversions.ConvertXElementToString(xXElement)
                              );
                      }


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



            //Console.WriteLine("exit GetInsertCommand " + new { c.CommandText });
            return c;
        }
        #endregion





        static QueryExpressionBuilder()
        {
            //Console.WriteLine("QueryExpressionBuilder.cctor");

            // x:\jsc.svn\examples\javascript\xml\xclickcounter\xclickcounter\application.cs
            // are we preinitialized by .Application?
            if (WithConnection == null)
                WithConnection =
                    y =>
                {
                    // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectXElement\Program.cs

                    // fake it
                    y(default(IDbConnection));
                };
        }

        [Obsolete("whats the default?")]
        public static Action<Action<IDbConnection>> WithConnection;


        public static void Insert<TElement>(this IQueryStrategy<TElement> source, params TElement[] collection)
        {
            // used by
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectAverage\Program.cs
            // x:\jsc.svn\examples\javascript\linq\test\auto\testselect\testweborderbythengroupby\application.cs

            WithConnection(
                  cc =>
                {
                    foreach (var item in collection)
                    {
                        Insert(source, cc, item);

                    }
                }
              );

        }

        public static void Insert<TElement>(this IQueryStrategy<TElement> source, TElement value)
        {

            WithConnection(
                cc =>
                {
                    Insert(source, cc, value);
                }
            );

        }

        [Obsolete("what should we return? rename to InsertSync ?")]
        public static void Insert<TElement>(this IQueryStrategy<TElement> source, IDbConnection cc, TElement value)
        {
            var c = GetInsertCommand(source, cc, value) as DbCommand;
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
