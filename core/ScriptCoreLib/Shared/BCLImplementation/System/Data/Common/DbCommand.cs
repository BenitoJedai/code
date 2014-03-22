using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using System.Data.Common;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data.Common
{
    [Script(Implements = typeof(global::System.Data.Common.DbCommand))]
    public abstract class __DbCommand : __Component, __IDbCommand
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140322

        public virtual DbDataReader __DbCommand_ExecuteReader()
        {
            return null;
        }



        public virtual object ExecuteScalar()
        {
            return null;
        }


        //Error	20	'ScriptCoreLib.Shared.BCLImplementation.System.Data.Common.__DbCommand'
        // does not implement interface member 'ScriptCoreLib.Shared.BCLImplementation.System.Data.__IDbCommand.ExecuteReader()'
        // . 'ScriptCoreLib.Shared.BCLImplementation.System.Data.Common.__DbCommand.ExecuteReader()' cannot implement 'ScriptCoreLib.Shared.BCLImplementation.System.Data.__IDbCommand.ExecuteReader()' because it does not have the matching return type of 'System.Data.IDataReader'.	X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Data\Common\DbCommand.cs	11	27	ScriptCoreLib


        public virtual DbDataReader ExecuteReader()
        {
            // tested by
            // X:\jsc.svn\examples\javascript\appengine\WebNotificationsViaDataAdapter\WebNotificationsViaDataAdapter\Schema\FooTable.cs
            // X:\jsc.svn\examples\javascript\appengine\AppEngineUserAgentLoggerWithXSLXAsset\AppEngineUserAgentLoggerWithXSLXAsset\ApplicationWebService.cs

            return __DbCommand_ExecuteReader();
        }

        public abstract int ExecuteNonQuery();
        public abstract string CommandText { get; set; }


        global::System.Data.IDataReader __IDbCommand.ExecuteReader()
        {
            return this.ExecuteReader();
        }

        public global::System.Data.IDbDataParameter CreateParameter()
        {

            return default(global::System.Data.IDbDataParameter);
        }
    }
}
