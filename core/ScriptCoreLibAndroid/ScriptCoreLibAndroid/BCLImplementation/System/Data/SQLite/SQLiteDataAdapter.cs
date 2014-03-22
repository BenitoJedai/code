using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
//using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android.BCLImplementation.System.Data.SQLite
{
    //[Script(Implements = typeof(global::System.Data.SQLite.SQLiteDataAdapter))]
    [Script(ImplementsViaAssemblyQualifiedName = "System.Data.SQLite.SQLiteDataAdapter")]
    internal class __SQLiteDataAdapter : __DbDataAdapter
    {
        public __SQLiteDataAdapter(__SQLiteCommand __SelectCommand)
        {
            Console.WriteLine("__SQLiteDataAdapter  " + new { __SelectCommand.CommandText });


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
                this.InternalSelectCommand = (DbCommand)(object)value;
            }
        }
    }

}
