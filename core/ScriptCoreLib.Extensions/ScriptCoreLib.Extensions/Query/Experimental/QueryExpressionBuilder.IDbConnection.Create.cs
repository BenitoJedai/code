using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ScriptCoreLib.Extensions;
using System.Reflection;
using System.Data;

namespace ScriptCoreLib.Query.Experimental
{
    public static partial class QueryExpressionBuilder
    {
        public static void Create<TElement>(this IQueryStrategy<TElement> source, IDbConnection cc)
        {
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

                    if (SourceBinding.Member.Name == "Key")
                    {
                        w.Append(" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT");
                        return;
                    }

                    var f = SourceBinding.Member as FieldInfo;
                    if (f.FieldType == typeof(string))
                    {
                        w.Append(" TEXT");
                        return;
                    }
                    if (f.FieldType == typeof(DateTime))
                    {
                        w.Append(" BIGINT NOT NULL");
                        return;
                    }
                    if (f.FieldType == typeof(long))
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

                    Debugger.Break();
                }
            );

            w.AppendLine(")");

            var c = cc.CreateCommand(CommandText: w.ToString());

            var n = c.ExecuteNonQuery();

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
        }


    }

}
