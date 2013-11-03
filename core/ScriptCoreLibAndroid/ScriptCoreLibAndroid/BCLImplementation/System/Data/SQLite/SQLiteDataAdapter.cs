using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android.BCLImplementation.System.Data.SQLite
{
    [Script(Implements = typeof(global::System.Data.SQLite.SQLiteDataAdapter))]
    internal class __SQLiteDataAdapter : __DbDataAdapter
    {
        public __SQLiteDataAdapter(SQLiteCommand __SelectCommand)
        {
            this.SelectCommand = __SelectCommand;
        }

        public new SQLiteCommand SelectCommand
        {
            get
            {
                return (SQLiteCommand)this.InternalSelectCommand;
            }
            set
            {
                this.InternalSelectCommand = value;
            }
        }
    }

}
