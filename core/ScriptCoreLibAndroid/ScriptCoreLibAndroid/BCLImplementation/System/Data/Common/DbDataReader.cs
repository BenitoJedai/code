using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Android.BCLImplementation.System.Data.Common
{
    [Script(Implements = typeof(global::System.Data.Common.DbDataReader))]
    internal abstract class __DbDataReaders : IDisposable
    {
        public abstract void Close();
        public abstract bool Read();

        public abstract object this[string name] { get; }

        public abstract int GetOrdinal(string name);
        public abstract string GetString(int i);
        public abstract int GetInt32(int i);



        public void Dispose()
        {
            this.Close();
        }
    }

}
