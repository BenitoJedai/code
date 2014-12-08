using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ScriptCoreLib.Extensions;
using System.Reflection;
using System.Data;
using System.Xml.Linq;

namespace ScriptCoreLib.Query.Experimental
{
    public enum QueryExpressionBuilderDialect { SQLite, MySQL };

    public static partial class QueryExpressionBuilder
    {
        // X:\jsc.svn\core\ScriptCoreLib\PHP\Data\SQLiteToMySQLConversion.cs


        [Obsolete("What about more automatic ways?")]
        public static QueryExpressionBuilderDialect Dialect = QueryExpressionBuilderDialect.SQLite;

        // to be called by the xlsx generated types inside ctor?
        public static IQueryStrategy<TElement> Create<TElement>(this IQueryStrategy<TElement> source)
        {
            // no need to call if database already has it, yet how would we know if we are stateless?

            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectMath\Program.cs


            // was it manually set?
            QueryExpressionBuilder.WithConnection(
                (IDbConnection cc) =>
                {
                    Create(source, cc); //.ContinueWith(z.SetResult);
                }
            );

            return source;
        }

        //public static IQueryStrategy<TElement> Create<TElement>(this IQueryStrategy<TElement> source, IDbConnection cc)
        public static IQueryStrategy<TElement> Create<TElement>(this IQueryStrategy<TElement> source, IDbConnection cc)
        {
            //Additional information: You have an error in your SQL syntax; check the manual that corresponds to your MySQL server version for the right syntax to use near 'AUTOINCREMENT,
            //`loadEventEnd` BIGINT NOT NULL,
            //`loadEventStart` BIGINT NOT NUL' at line 7


            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestXMySQL\Program.cs
            // X:\jsc.svn\examples\javascript\test\TestMemberInitExpression\TestMemberInitExpression\Application.cs
            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectMath\Program.cs

            var xSelect = source as xSelect;

            var xMemberInitExpression = xSelect.selector.Body as MemberInitExpression;

            var w = new StringBuilder();

            w.AppendLine("create table if not exists `" + xSelect.selector.Parameters[0].Name + "` (");

            xMemberInitExpression.Bindings.WithEachIndex(
                (SourceBinding, i) =>
                {
                    if (i > 0)
                        w.AppendLine(",");

                    w.Append("`" + SourceBinding.Member.Name + "`");

                    #region  Key, also referenced by keyselector
                    if (SourceBinding.Member.Name == "Key")
                    {
                        w.Append(" INTEGER NOT NULL PRIMARY KEY");

                        if (Dialect == QueryExpressionBuilderDialect.SQLite)
                            w.Append(" AUTOINCREMENT");
                        else
                            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestXMySQL\Program.cs
                            w.Append(" AUTO_INCREMENT");

                        return;
                    }
                    #endregion

                    var xMemberAssignment = SourceBinding as MemberAssignment;
                    var xUnaryExpression = xMemberAssignment.Expression as UnaryExpression;
                    var f = SourceBinding.Member as FieldInfo;

                    var FieldType = default(Type);

                    if (xUnaryExpression == null)
                        FieldType = f.FieldType;
                    else
                        FieldType = xUnaryExpression.Type;


                    // do we have a test for that yet?
                    if (FieldType == typeof(XElement))
                    {
                        w.Append(" TEXT");
                        return;
                    }

                    if (FieldType == typeof(string))
                    {
                        w.Append(" TEXT");
                        return;
                    }

                    if (FieldType == typeof(DateTime))
                    {
                        w.Append(" BIGINT NOT NULL");
                        return;
                    }
                    if (FieldType == typeof(long))
                    {
                        w.Append(" BIGINT NOT NULL");
                        return;
                    }


                    // x:\jsc.svn\examples\javascript\linq\ggearalpha\ggearalpha\library\googlegearsadvanced.cs
                    // this we now seem to have, in chrome.. :)
                    if (f.FieldType == typeof(int))
                    {
                        w.Append(" BIGINT NOT NULL");
                        return;
                    }

                    if (f.FieldType == typeof(bool))
                    {
                        w.Append(" BIGINT NOT NULL");
                        return;
                    }

                    // foreign keys
                    if (f.FieldType.IsEnum)
                    {
                        w.Append(" BIGINT NOT NULL");
                        return;
                    }

                    // https://www.sqlite.org/datatype3.html
                    // X:\jsc.svn\examples\javascript\test\TestXLSXDouble\TestXLSXDouble\ApplicationWebService.cs
                    // X:\jsc.svn\examples\javascript\appengine\XSLXAssetWithXElement\XSLXAssetWithXElement\ApplicationWebService.cs
                    if (f.FieldType == typeof(double))
                    {
                        w.Append(" REAL NOT NULL");
                        return;
                    }

                    Debugger.Break();
                }
            );

            w.AppendLine(")");




            if (cc != null)
            {
                var c = cc.CreateCommand(CommandText: w.ToString());

                var n = c.ExecuteNonQuery();
            }

            //Console.WriteLine(new { n });

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


            return source;
        }


    }

}
