using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Data.SQLite
{
    [Script(ImplementsViaAssemblyQualifiedName = "System.Data.SQLite.SQLiteParameterCollection")]
    public class __SQLiteParameterCollection : __DbParameterCollection
    {
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Data\SQLite\SQLiteParameterCollection.cs



        public static implicit operator DbParameterCollection(__SQLiteParameterCollection e)
        {
            return (DbParameterCollection)(object)e;
        }

        public override int Count
        {
            get { throw new NotImplementedException(); }
        }
    }
}
