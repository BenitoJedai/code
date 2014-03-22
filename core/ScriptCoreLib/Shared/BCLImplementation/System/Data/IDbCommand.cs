using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data
{
    [Script(Implements = typeof(global::System.Data.IDbCommand))]
    internal interface __IDbCommand : IDisposable
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140322

        IDbDataParameter CreateParameter();

        IDataReader ExecuteReader();

        int ExecuteNonQuery();
        string CommandText { get; set; }
    }
}
