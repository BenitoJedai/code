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
            var p = x.CreateParameter();
            p.ParameterName = ParameterName;
            p.Value = Value;

            //  java.lang.NullPointerException
            //at System.Data.IDbConnectionExtensions.AddParameter(IDbConnectionExtensions.java:30)
            //at SQLiteWithDataGridView.Schema.TheGridTableExtensions.ExecuteReader(TheGridTableExtensions.java:55)
            //at

            var Parameters = x.Parameters;

            Parameters.Add(p);
            return x;
        }
    }
}
