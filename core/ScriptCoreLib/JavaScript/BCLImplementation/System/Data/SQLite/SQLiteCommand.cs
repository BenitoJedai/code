using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Data.SQLite
{
    [Script(ImplementsViaAssemblyQualifiedName = "System.Data.SQLite.SQLiteCommand")]
    public class __SQLiteCommand : __DbCommand
    {
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Data\SQLite\SQLiteCommand.cs
        public override string CommandText { get; set; }
        __SQLiteConnection c;

        public __SQLiteCommand(string sql, __SQLiteConnection c)
        {
            this.c = c;
            this.CommandText = sql;
        }

        public override int ExecuteNonQuery()
        {
            Console.WriteLine(new { CommandText });

            return 0;
        }

        public new __SQLiteParameterCollection Parameters { get; set; }

        public override DbParameterCollection DbParameterCollection
        {
            get { return this.Parameters; }
        }

    }
}
