using System;
using System.Collections.Generic;
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

        public global::System.Data.IDbTransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }
    }
}
