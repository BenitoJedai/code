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
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Data\SQLite\SQLiteDataAdapter.cs

        public __SQLiteDataAdapter(DbCommand __SelectCommand)
        {
            this.SelectCommand = __SelectCommand;
        }

    }

}
