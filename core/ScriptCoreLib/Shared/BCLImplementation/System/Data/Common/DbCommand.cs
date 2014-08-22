using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using System.Data.Common;
using System.Threading.Tasks;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data.Common
{
    // http://referencesource.microsoft.com/#System.Data/data/System/Data/Common/DBCommand.cs
    // https://github.com/mono/mono/blob/a31c107f59298053e4ff17fd09b2fa617b75c1ba/mcs/class/System.Data/System.Data.Common/DbCommand.cs

    [Script(Implements = typeof(global::System.Data.Common.DbCommand))]
    public abstract class __DbCommand : __Component, __IDbCommand
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140322

        #region Parameters
        public abstract DbParameterCollection DbParameterCollection { get; }

        public DbParameterCollection Parameters { get { return this.DbParameterCollection; } }

        global::System.Data.IDataParameterCollection __IDbCommand.Parameters
        {
            get { return this.DbParameterCollection; }
        }
        #endregion


        public virtual Task<DbDataReader> __DbCommand_ExecuteReaderAsync()
        {
            return null;
        }
        public virtual DbDataReader __DbCommand_ExecuteReader()
        {
            return null;
        }



        public virtual object ExecuteScalar()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140822

            return null;
        }


        public virtual Task<object> ExecuteScalarAsync()
        {
            return null;
        }

        public virtual Task<DbDataReader> ExecuteReaderAsync()
        {
            // X:\jsc.svn\examples\javascript\test\TestSQLiteConnection\TestSQLiteConnection\Application.cs
            return __DbCommand_ExecuteReaderAsync();
        }

        public virtual DbDataReader ExecuteReader()
        {
            // tested by
            // X:\jsc.svn\examples\javascript\appengine\WebNotificationsViaDataAdapter\WebNotificationsViaDataAdapter\Schema\FooTable.cs
            // X:\jsc.svn\examples\javascript\appengine\AppEngineUserAgentLoggerWithXSLXAsset\AppEngineUserAgentLoggerWithXSLXAsset\ApplicationWebService.cs

            return __DbCommand_ExecuteReader();
        }


        // X:\jsc.svn\examples\javascript\Test\TestWebSQLDatabase\TestWebSQLDatabase\Application.cs
        // X:\jsc.svn\examples\javascript\test\TestSQLiteConnection\TestSQLiteConnection\Application.cs
        public virtual Task<int> ExecuteNonQueryAsync()
        {
            throw new NotImplementedException();
        }

        public abstract int ExecuteNonQuery();
        public abstract string CommandText { get; set; }


        global::System.Data.IDataReader __IDbCommand.ExecuteReader()
        {
            return this.ExecuteReader();
        }



        public virtual DbParameter CreateDbParameter()
        {
            return default(global::System.Data.Common.DbParameter);
        }

        public global::System.Data.IDbDataParameter CreateParameter()
        {

            return CreateDbParameter();
        }


    }
}
