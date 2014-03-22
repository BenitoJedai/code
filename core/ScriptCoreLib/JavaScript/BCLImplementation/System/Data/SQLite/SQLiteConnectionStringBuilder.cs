using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Data.SQLite
{
    //[Script(Implements = typeof(global::System.Data.SQLite.SQLiteConnectionStringBuilder))]
    public class __SQLiteConnectionStringBuilder : __DbConnectionStringBuilder
    {
        // tested by?
        // chrome web server?

        public string DataSource { get; set; }

        public int Version { get; set; }

        public string ConnectionString { get; set; }
    }
}
