using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

//namespace ScriptCoreLib.Extensions
namespace System.Data
{
    public static class IDbConnectionExtensions
    {
        public static IDbCommand CreateCommand(this IDbConnection c, string CommandText)
        {
            // X:\jsc.svn\examples\javascript\forms\SQLiteWithDataGridView\SQLiteWithDataGridView\Schema\TheGridTable.cs
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140318

            var x = c.CreateCommand();
            x.CommandText = CommandText;

            return x;
        }

        public static IDbCommand AddParameter(this IDbCommand x, string ParameterName, object Value)
        {
            Console.WriteLine("AddParameter enter " + new { x, ParameterName });
            var newParameter = x.CreateParameter();
            Console.WriteLine("AddParameter " + new { newParameter });

            //java.lang.NullPointerException
            //   at System.Data.IDbConnectionExtensions.AddParameter(IDbConnectionExtensions.java:39)
            //   at SQLiteWithDataGridView.Schema.TheGridTableExtensions.ExecuteReader(TheGridTableExtensions.java:50)
            //   at SQLiteWithDataGridView.Schema.TheGridTable___c__DisplayClassf._SelectContent_b__e(TheGridTable___c__DisplayClassf.java:27)


            newParameter.ParameterName = ParameterName;
            newParameter.Value = Value;

            //  java.lang.NullPointerException
            //at System.Data.IDbConnectionExtensions.AddParameter(IDbConnectionExtensions.java:30)
            //at SQLiteWithDataGridView.Schema.TheGridTableExtensions.ExecuteReader(TheGridTableExtensions.java:55)
            //at

            var Parameters = x.Parameters;

            Console.WriteLine("AddParameter " + new { Parameters });

            Parameters.Add(newParameter);
            return x;
        }
    }
}
