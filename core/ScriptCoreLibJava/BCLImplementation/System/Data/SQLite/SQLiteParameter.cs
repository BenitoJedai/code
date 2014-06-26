using ScriptCoreLib;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
//using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace ScriptCoreLibJava.BCLImplementation.System.Data.SQLite
{
    //[Script(Implements = typeof(global::System.Data.SQLite.SQLiteParameter))]
    [Script(ImplementsViaAssemblyQualifiedName = "System.Data.SQLite.SQLiteParameter")]
    internal class __SQLiteParameter : __DbParameter
    {
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Data\SQLite\SQLiteParameter.cs

        public override string ParameterName { get; set; }
        public override object Value { get; set; }
    }
}
