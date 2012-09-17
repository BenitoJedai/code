using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android.BCLImplementation.System.Data.Common
{
    [Script(Implements = typeof(global::System.Data.Common.DbConnection))]
    public abstract class __DbConnection : global::System.IDisposable
    {
        public abstract void Open();
        public abstract void Close();

        public abstract void Dispose();
    }
}
