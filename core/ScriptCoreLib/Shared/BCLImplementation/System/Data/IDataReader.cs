using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data
{
    [Script(Implements = typeof(global::System.Data.IDataReader))]
    internal interface __IDataReader : IDisposable, __IDataRecord
    {
        void Close();
        bool Read();
    }
}
