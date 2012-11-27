using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data
{
    [Script(Implements = typeof(global::System.Data.IDataRecord))]
    internal interface __IDataRecord
    {
        long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length);

        int GetOrdinal(string name);

        int FieldCount { get; }

        object this[string name] { get; }
        //object this[int i] { get; }
    }
}
