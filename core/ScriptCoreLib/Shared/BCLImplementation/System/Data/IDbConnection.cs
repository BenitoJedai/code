using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data
{
    [Script(Implements = typeof(global::System.Data.IDbConnection))]
    internal interface __IDbConnection : IDisposable
    {
        IDbCommand CreateCommand();

        IDbTransaction BeginTransaction();
        void Open();
        void Close();


    }
}
