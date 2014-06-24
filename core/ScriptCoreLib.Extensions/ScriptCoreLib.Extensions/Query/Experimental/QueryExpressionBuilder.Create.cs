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
        public static void Create<TElement>(this IQueryStrategy<TElement> source)
        {

            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectMath\Program.cs

            var xSelect = source as xSelect;

            var xMemberInitExpression = xSelect.selector.Body as MemberInitExpression;

            Console.WriteLine("create table if not exists `" + xSelect.selector.Parameters[0].Name + "` (");

            xMemberInitExpression.Bindings.WithEachIndex(
                (SourceBinding, i) =>
                {
                    if (i > 0)
                        Console.WriteLine(",");

                    Console.Write("`" + SourceBinding.Member.Name + "`");

                    if (SourceBinding.Member.Name == "Key")
                    {
                        Console.Write(" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT");
                        return;
                    }

                    var f = SourceBinding.Member as FieldInfo;
                    if (f.FieldType == typeof(string))
                    {
                        Console.Write(" TEXT");
                        return;
                    }
                    if (f.FieldType == typeof(DateTime))
                    {
                        Console.Write(" BIGINT NOT NULL");
                        return;
                    }
                    if (f.FieldType == typeof(long))
                    {
                        Console.Write(" BIGINT NOT NULL");
                        return;
                    }
                    Debugger.Break();
                }
            );

            Console.WriteLine(")");

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
