using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data.Common
{
    [Script(Implements = typeof(global::System.Data.Common.DbDataReader))]
    public abstract class __DbDataReader : IDisposable, __IDataReader
    {
        public abstract void Close();
        public abstract bool Read();

        public abstract object this[string name] { get; }
        public virtual object this[int i] { get { return null; } }

        public abstract int GetOrdinal(string name);
        public abstract string GetString(int i);
        public abstract int GetInt32(int i);
        public abstract string GetName(int ordinal);
        public abstract long GetInt64(int ordinal);
        public abstract double GetDouble(int ordinal);

        public abstract Type GetFieldType(int ordinal);
        public abstract int FieldCount { get; }

        public void Dispose()
        {
            this.Close();
        }

        public abstract long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length);

        public bool IsClosed
        {
            get;
            set;
        }

        public int RecordsAffected
        {
            get;
            set;
        }

        public static implicit operator global::System.Data.Common.DbDataReader(__DbDataReader r)
        {
            return (global::System.Data.Common.DbDataReader)(object)r;
        }

    }

}
