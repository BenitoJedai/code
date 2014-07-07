using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

//namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Data.SQLite
//namespace ScriptCoreLibJava.BCLImplementation.System.Data.SQLite
namespace ScriptCoreLib.Shared.BCLImplementation.System.Data.SQLite
{
    [Script(ImplementsViaAssemblyQualifiedName = "System.Data.SQLite.SQLiteParameter")]
    public class __SQLiteParameter : __DbParameter
    {
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Data\SQLite\SQLiteParameterCollection.cs

        public override string ParameterName { get; set; }
        public override object Value { get; set; }
    }
}
