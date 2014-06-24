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

namespace ScriptCoreLib.Query.Experimental
{
    public static partial class QueryExpressionBuilder
    {
        public static DataTable AsDataTable<TElement>(this IQueryStrategy<TElement> source, IDbConnection cc)
        {

            // X:\jsc.svn\examples\javascript\LINQ\test\auto\TestSelect\TestSelectMath\Program.cs


            var w = new SQLWriter<TElement>(source, new IQueryStrategy[0].AsEnumerable(), cc: cc);


            var a = new __DbDataAdapter { SelectCommand = w.Command };

            var t = new DataTable();
            a.Fill(t);

            return t;
        }


    }

}
