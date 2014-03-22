using ScriptCoreLib;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
//using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace ScriptCoreLibJava.BCLImplementation.System.Data.SQLite
{
    //[Script(Implements = typeof(global::System.Data.SQLite.SQLiteDataAdapter))]
    [Script(ImplementsViaAssemblyQualifiedName = "System.Data.SQLite.SQLiteDataAdapter")]
    internal class __SQLiteDataAdapter : __DbDataAdapter
    {
        public __SQLiteDataAdapter(__SQLiteCommand __SelectCommand)
        {
            this.SelectCommand = __SelectCommand;
        }

        public new __SQLiteCommand SelectCommand
        {
            get
            {
                return (__SQLiteCommand)(object)this.InternalSelectCommand;
            }
            set
            {
                this.InternalSelectCommand = (global::System.Data.Common.DbCommand)(object)value;
            }
        }
    }
}
