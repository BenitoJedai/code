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

//        Implementation not found for type import :
//type: System.Data.Common.DbCommand
//method: System.Object ExecuteScalar()
//Did you forget to add the [Script] attribute?
//Please double check the signature!

        public virtual object ExecuteScalar()
        {
            return null;
        }

        public DbDataReader ExecuteReader()
        {
            // tested by
            // X:\jsc.svn\examples\javascript\appengine\WebNotificationsViaDataAdapter\WebNotificationsViaDataAdapter\Schema\FooTable.cs
            // X:\jsc.svn\examples\javascript\appengine\AppEngineUserAgentLoggerWithXSLXAsset\AppEngineUserAgentLoggerWithXSLXAsset\ApplicationWebService.cs

            return __DbCommand_ExecuteReader();
        }

        public abstract int ExecuteNonQuery();
        public abstract string CommandText { get; set; }

    }
}
