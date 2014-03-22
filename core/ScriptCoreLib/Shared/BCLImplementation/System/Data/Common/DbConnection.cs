using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data.Common
{
    [Script(Implements = typeof(global::System.Data.Common.DbConnection))]
    public abstract class __DbConnection : __IDbConnection, global::System.IDisposable
    {
        public abstract string ConnectionString { get; set; }

        public abstract void Open();
        public abstract void Close();

        public abstract void Dispose();

        public DbCommand CreateCommand()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140322

            return CreateDbCommand();
        }

        protected virtual DbCommand CreateDbCommand()
        {
            return default(DbCommand);
        }

        public global::System.Data.IDbTransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }

        global::System.Data.IDbCommand __IDbConnection.CreateCommand()
        {
            return this.CreateCommand();
        }
    }
}
