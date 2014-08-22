using ScriptCoreLib;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
//using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace ScriptCoreLibJava.BCLImplementation.System.Data.SQLite
{
    //[Script(Implements = typeof(global::System.Data.SQLite.SQLiteDataAdapter))]
    [Script(ImplementsViaAssemblyQualifiedName = "System.Data.MySQL.MySQLDataAdapter")]
    internal class __MySQLDataAdapter : __DbDataAdapter
    {
        // message = "\n\n Implementation not found for type import : \n type: System.Data.SQLite.SQLiteDataAdapter\n method: 
        // Void .ctor(System.Data.Common.DbCommand)\n Did you forget to add the [Script] attribute? \n Please double check the signature! \n \n assembly: W:\\XSLXA...

        // tested by
        // X:\jsc.svn\examples\javascript\appengine\XSLXAssetWithXElement\XSLXAssetWithXElement\ApplicationWebService.cs

        public __MySQLDataAdapter(DbCommand __SelectCommand)
        {
            this.SelectCommand = __SelectCommand;
        }

    }
}
