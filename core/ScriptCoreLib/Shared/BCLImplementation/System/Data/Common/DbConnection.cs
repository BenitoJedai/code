using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data.Common
{
    // http://referencesource.microsoft.com/#System.Data/data/System/Data/Common/DBConnection.cs
    // https://github.com/mono/mono/blob/a31c107f59298053e4ff17fd09b2fa617b75c1ba/mcs/class/System.Data/System.Data.Common/DbConnection.cs

    [Script(Implements = typeof(global::System.Data.Common.DbConnection))]
    public abstract class __DbConnection : __Component, __IDbConnection, global::System.IDisposable
    {
        public abstract string ConnectionString { get; set; }

        public abstract void Open();
        public abstract void Close();

        //public virtual void Dispose()
        //{
        //    //
        //}

        public override void Dispose(bool e)
        {
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Data\SQLite\SQLiteConnection.cs
            this.Close();
        }

        public DbCommand CreateCommand()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140322

            return CreateDbCommand();
        }

        public virtual DbCommand CreateDbCommand()
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
