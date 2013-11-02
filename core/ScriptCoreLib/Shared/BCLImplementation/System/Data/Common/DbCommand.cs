using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using System.Data.Common;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data.Common
{
    [Script(Implements = typeof(global::System.Data.Common.DbCommand))]
    public abstract class __DbCommand : __Component
    {
        public virtual DbDataReader __DbCommand_ExecuteReader()
        {
            return null;
        }

        public DbDataReader ExecuteReader()
        {
            // tested by
            // X:\jsc.svn\examples\javascript\appengine\WebNotificationsViaDataAdapter\WebNotificationsViaDataAdapter\Schema\FooTable.cs

            return __DbCommand_ExecuteReader(); ;
        }

        public abstract int ExecuteNonQuery();
        public abstract string CommandText { get; set; }

    }
}
