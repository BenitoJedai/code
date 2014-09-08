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

namespace ScriptCoreLib.Query.Experimental
{
    public static partial class QueryExpressionBuilder
    {
        public static DataTable AsDataTable<TElement>(this IQueryStrategy<TElement> source)
        {
            // X:\jsc.svn\examples\javascript\appengine\AppEngineUserAgentLoggerWithXSLXAsset\AppEngineUserAgentLoggerWithXSLXAsset\ApplicationWebService.cs

            var value = default(DataTable);

            // what if there is no connection?
            WithConnection(
                cc =>
                {
                    value = AsDataTable(source, cc);
                }
            );

            return value;
        }

        public static DataTable AsDataTable<TElement>(this IQueryStrategy<TElement> source, IDbConnection cc)
        {

            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectMath\Program.cs

            var c = (DbCommand)cc.CreateCommand();

            var w = new SQLWriter<TElement>(source, new IQueryStrategy[0].AsEnumerable(), Command: c);


            var a = new __DbDataAdapter { SelectCommand = c };

            var t = new DataTable();
            a.Fill(t);

            return t;
        }


    }

}
