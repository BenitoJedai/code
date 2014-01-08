using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Data.SQLite
{
    [Script(Implements = typeof(global::System.Data.SQLite.SQLiteConnection))]
    public class __SQLiteConnection : __DbConnection
    {
        public override string ConnectionString { get; set; }

        public __SQLiteConnection(string connectionstring)
        {
        }

        public override void Open()
        {
        }

        public override void Close()
        {
        }

        public override void Dispose()
        {
        }
    }
}
